using GameDev_Project.AnimationLogic;
using GameDev_Project.Interfaces;
using GameDev_Project.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameDev_Project.Characters.Enemies
{
    public class Enemy : Character, IGameObject
    {
        public Hero hero;
        private const float _followRange = 300.0f;
        public Texture2D enemyTexture;

        private Animation _idleAnimation;
        private Animation _walkingAnimation;

        public float direction;

        public Enemy(Vector2 startPosition, Texture2D enemyTexture, GraphicsDevice graphicsDevice, Hero hero)
        {
            this.hero = hero;

            Width = 64;
            Height = 64;

            this.enemyTexture = enemyTexture;
            boundingBoxTexture = new Texture2D(graphicsDevice, 1, 1);
            boundingBoxTexture.SetData(new[] { Color.White });
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

            currentAnimation = _idleAnimation;
        }
        public override void Move()
        {
            //===Follow logic===
            if (Math.Abs(Position.X - hero.Position.X) <= _followRange)
            {
                direction = Math.Sign(hero.Position.X - Position.X); // -1 for left, +1 for right
                Speed = new Vector2(direction * MaxHorizontalSpeed, Speed.Y);
            }
            else
            {
                Speed = new Vector2(0, Speed.Y);
            }
            base.Move();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(enemyTexture, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), horizontalFlip, 0f);
        }

        public override void Update(GameTime gameTime)
        {
            Move();
            if (direction == 0)
            {
                currentAnimation = _idleAnimation;
            }
            else
            {
                currentAnimation = _walkingAnimation;
                if (direction == -1)
                    horizontalFlip = SpriteEffects.FlipHorizontally;
                else
                    horizontalFlip = SpriteEffects.None;
            }

            // Update BoundingBox position
            BoundingBox = new Rectangle((int)Position.X + 15, (int)Position.Y + 16, Width - 35, Height - 30);

            // Decrement ImmunityTimer
            if (ImmunityTimer > 0)
            {
                ImmunityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            Attack(Scene.Hero);
            currentAnimation.Update(gameTime);
            base.Update(gameTime);
        }

        #region Animations
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
        #endregion
    }
}
