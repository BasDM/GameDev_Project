using GameDev_Project.AnimationLogic;
using GameDev_Project.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.Characters.Enemies
{
    public class RunawayEnemy : Character
    {
        private const float _runawayRange = 200.0f;
        private Animation _runAnimation;
        private Hero _hero;
        public float _direction;
        private Texture2D _enemyTexture;
        
        public RunawayEnemy(Vector2 startPosition, Texture2D enemyTexture, GraphicsDevice graphicsDevice, Hero hero)
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
            MaxVerticalSpeed = 80;

            Health = 1;
            MaxHealth = Health;
            Dead = false;
            Gravity = 1f;
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            AddRunAnimation();

            currentAnimation = _runAnimation;
        }
        public override void Update(GameTime gameTime)
        {
            Move();
            if (_direction == 0)
            {
                currentAnimation = _runAnimation;
            }
            else
            {
                currentAnimation = _runAnimation;
                if (_direction == -1)
                    horizontalFlip = SpriteEffects.FlipHorizontally;
                else
                    horizontalFlip = SpriteEffects.None;
            }

            // Update BoundingBox position
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            // Decrement ImmunityTimer
            if (ImmunityTimer > 0)
            {
                ImmunityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            currentAnimation.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Move()
        {
            //===Runaway Logic===
            if (Math.Abs(Position.X - _hero.Position.X) <= _runawayRange)
            {
                _direction = Math.Sign(_hero.Position.X - Position.X );
                Speed = new Vector2(-_direction * MaxHorizontalSpeed, Speed.Y);
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
            spriteBatch.Draw(_enemyTexture, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), currentAnimation.CurrentFrame.SourceRectangle, Color.White,0, new Vector2(0, 0), horizontalFlip,0f);
        }

        #region Animations
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
