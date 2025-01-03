﻿using GameDev_Project.AnimationLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameDev_Project.Characters.Enemies
{
    public class FlyingEnemy : Character
    {
        private const float _flyRange = 150.0f;
        private Random _random;
        private Texture2D _enemyTexture;
        private Animation _flyAnimation;
        private Hero _hero;
        private Vector2 _direction;

        public FlyingEnemy(Vector2 startPosition, Texture2D enemyTexture, GraphicsDevice graphicsDevice, Hero hero)
        {
            _hero = hero;

            Width = 32;
            Height = 32;

            _enemyTexture = enemyTexture;
            boundingBoxTexture = new Texture2D(graphicsDevice, 1, 1);
            boundingBoxTexture.SetData(new[] { Color.White });
            Position = startPosition;

            Speed = new Vector2(0, 0);
            Acceleration = new Vector2(0.9f, 0.9f);
            MaxHorizontalSpeed = 2;
            MaxVerticalSpeed = 2;

            Health = 1;
            MaxHealth = Health;
            Dead = false;
            Gravity = 0f; // No gravity
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            _random = new Random();
            AddFlyAnimation();

            currentAnimation = _flyAnimation;
        }

        public override void Update(GameTime gameTime)
        {
            Move();
            if (Math.Sign(_hero.Position.X - Position.X) > 0)
                horizontalFlip = SpriteEffects.FlipHorizontally;
            else
                horizontalFlip = SpriteEffects.None;
            currentAnimation.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Move()
        {
            float distanceToPlayer = Vector2.Distance(Position, _hero.Position);
            if (distanceToPlayer > _flyRange)
            {
                _direction = Vector2.Normalize(_hero.Position - Position);
                Speed = _direction * new Vector2(MaxHorizontalSpeed, MaxVerticalSpeed);
            }
            else
            {
                Speed = new Vector2((float)_random.NextDouble() * MaxHorizontalSpeed, 0);
            }

            Position += Speed;
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            base.Move();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(_enemyTexture, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), horizontalFlip, 0f);
        }

        #region Animations
        public void AddFlyAnimation()
        {
            _flyAnimation = new Animation();
            for (int i = 0; i < 23; i++)
            {
                _flyAnimation.AddFrame(new AnimationFrame(new Rectangle(Width * i, 0, Width, Height)));
            }
        }
        #endregion
    }
}
