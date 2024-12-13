using GameDev_Project.AnimationLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.Characters.Enemies
{
    public class RunawayEnemy : Enemy
    {
        private const float _runawayRange = 200.0f;
        private Animation _idleAnimation;
        private Animation _runAnimation;
        
        public RunawayEnemy(Vector2 startPosition, Texture2D enemyTexture, GraphicsDevice graphicsDevice, Hero hero) : base(startPosition, enemyTexture, graphicsDevice, hero)
        {
            _hero = hero;
            Width = 16;
            Height = 16;

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
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            AddIdleAnimation();
            AddRunAnimation();

            currentAnimation = _idleAnimation;
        }
        public void Update()
        {
            Move();
            if (_direction == 0)
            {
                currentAnimation = _idleAnimation;
            }
            else
            {
                currentAnimation = _runAnimation;
                if (_direction == -1)
                    horizontalFlip = SpriteEffects.FlipHorizontally;
                else
                    horizontalFlip = SpriteEffects.None;
            }
        }
        public override void Move()
        {
            //===Runaway Logic===
            if (Math.Abs(Position.X - _hero.Position.X) <= _runawayRange)
            {
                _direction = Math.Sign(_hero.Position.X + Position.X);
                Speed = new Vector2(_direction * MaxHorizontalSpeed, Speed.Y);
            }
            else
            {
                Speed = new Vector2(0, Speed.Y);
            }
            base.Move();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Debug)
            {
                spriteBatch.Draw(Texture, BoundingBox, Color.Red);
            }
            spriteBatch.Draw(_enemyTexture, new Rectangle((int)Position.X, (int)Position.Y, 64, 64), currentAnimation.CurrentFrame.SourceRectangle, Color.White,0, new Vector2(0, 0), horizontalFlip,0f);
        }

        #region Animations
        public void AddIdleAnimation()
        {
            _idleAnimation = new Animation();
            for (int i = 0; i < 6; i++)
            {
                _idleAnimation.AddFrame(new AnimationFrame(new Rectangle(Width * i, 16, Width, Height)));
            }
        }
        public void AddRunAnimation()
        {
            _runAnimation = new Animation();
            for (int i = 0; i < 8; i++)
            {
                _runAnimation.AddFrame(new AnimationFrame(new Rectangle(Width * i, 0, Width, Height)));
            }
        }
        #endregion
    }
}
