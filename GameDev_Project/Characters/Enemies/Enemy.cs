using GameDev_Project.AnimationLogic;
using GameDev_Project.Events;
using GameDev_Project.Interfaces;
using GameDev_Project.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameDev_Project.Characters.Enemies
{
    public class Enemy : Character, IGameObject
    {
        private Hero _hero;
        private const float _followRange = 300.0f;
        private Texture2D _enemyTexture;

        private Animation _currentAnimation;
        private Animation _idleAnimation;
        private Animation _walkingAnimation;

        private float _direction;

        public Enemy(Vector2 startPosition, Texture2D enemyTexture, GraphicsDevice graphicsDevice, Hero hero)
        {
            _hero = hero;

            Width = 64;
            Height = 64;

            _enemyTexture = enemyTexture;
            Texture = new Texture2D(graphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.White });
            Position = startPosition;

            Speed = new Vector2(0, 0);
            Acceleration = new Vector2(0.9f, 0.9f);
            MaxHorizontalSpeed = 2;
            MaxVerticalSpeed = 80;

            Health = 2;
            MaxHealth = Health;
            Dead = false;
            Gravity = 1f;
            BoundingBox = new Rectangle((int)Position.X + 15, (int)Position.Y + 16, Width - 35, Height - 30);

            AddIdleAnimation();
            AddWalkingAnimation();
        }
        public void Move()
        {
            //===Follow logic===
            if (Math.Abs(Position.X - _hero.Position.X) <= _followRange)
            {
                _direction = Math.Sign(_hero.Position.X - Position.X); // -1 for left, +1 for right
                Speed = new Vector2(_direction * MaxHorizontalSpeed, Speed.Y);
            }
            else
            {
                Speed = new Vector2(0, Speed.Y);
            }

            Speed = new Vector2(Speed.X, Speed.Y + Gravity);
            Speed = new Vector2(Math.Clamp(Speed.X, -4, MaxHorizontalSpeed), Math.Clamp(Speed.Y, -30, MaxVerticalSpeed));

            float horizontalMovement = Speed.X;
            Rectangle futureHorizontalBoundingBox = new Rectangle(
                (int)(BoundingBox.X + horizontalMovement),
                BoundingBox.Y,
                BoundingBox.Width,
                BoundingBox.Height
            );

            List<ICollidable> horizontalCollidables = CollisionHandler.CollidingWithObject(futureHorizontalBoundingBox);
            if (horizontalCollidables.Any(o => futureHorizontalBoundingBox.Intersects(o.BoundingBox)))
            {
                horizontalMovement = 0;
            }

            float verticalMovement = Speed.Y;
            Rectangle futureVerticalBoundingBox = new Rectangle(
                BoundingBox.X,
                (int)(BoundingBox.Y + verticalMovement),
                BoundingBox.Width,
                BoundingBox.Height
            );

            List<ICollidable> verticalCollidables = CollisionHandler.CollidingWithObject(futureVerticalBoundingBox);
            if (verticalCollidables.Any(o => futureVerticalBoundingBox.Intersects(o.BoundingBox) && Position.Y <= o.BoundingBox.Top))
                verticalMovement = 0;
            else
                verticalMovement += Gravity;

            // Update speed and position
            Speed = new Vector2(horizontalMovement, verticalMovement);
            Position += Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Debug)
            {
                spriteBatch.Draw(Texture, BoundingBox, Color.Red);
            }
            spriteBatch.Draw(_enemyTexture, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), _currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), horizontalFlip, 0f);
        }

        public override void Update(GameTime gameTime)
        {
            Move();
            if (_direction == 0)
            {
                _currentAnimation = _idleAnimation;
            }
            else
            {
                _currentAnimation = _walkingAnimation;
                if (_direction == -1)
                    horizontalFlip = SpriteEffects.FlipHorizontally;
                else
                    horizontalFlip = SpriteEffects.None;
            }

            if (BoundingBox.Intersects(GameScene.Hero.BoundingBox) && GameScene.Hero.ImmunityTimer <= 0)
            {
                GameScene.Hero.GetHit(1);
            }

            _currentAnimation.Update(gameTime);

            // Update BoundingBox position
            BoundingBox = new Rectangle((int)Position.X + 15, (int)Position.Y + 16, Width - 35, Height - 30);

            // Decrement ImmunityTimer
            if (ImmunityTimer > 0)
            {
                ImmunityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void AddIdleAnimation()
        {
            _idleAnimation = new Animation();
            for (int i = 0; i < 4; i++)
            {
                _idleAnimation.AddFrame(new AnimationFrame(new Rectangle(Width * i, 192, Width, Height)));
            }
        }

        public void AddWalkingAnimation()
        {
            _walkingAnimation = new Animation();
            for (int i = 0; i < 12; i++)
            {
                _walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(Width * i, 128, Width, Height)));
            }
        }
    }
}
