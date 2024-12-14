using GameDev_Project.AnimationLogic;
using GameDev_Project.Events;
using GameDev_Project.Interfaces;
using GameDev_Project.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameDev_Project.Characters
{
    public class Hero : Character, IGameObject
    {
        #region Variables
        Texture2D _heroTexture;
        Animation _walkingAnimation;
        Animation _idleAnimation;
        Animation _deathAnimation;
        Animation _attackAnimation;

        //jump vars
        private int _counter = 0;
        private bool _isJumping = false;

        private IInputReader _inputReader;
        #endregion
        
        public Hero(Texture2D texture, IInputReader inputReader, GraphicsDevice graphicsDevice)
        {
            IsOnGround = false;
            Position = new Vector2(0, 20);
            Speed = new Vector2(0, 0);
            Acceleration = new Vector2(0.9f, 0.9f);
            MaxVerticalSpeed = 80;
            MaxHorizontalSpeed = 4;

            Width = 80;
            Height = 80;

            _heroTexture = texture;
            this._inputReader = inputReader;
            boundingBoxTexture = new Texture2D(graphicsDevice, 1, 1);
            boundingBoxTexture.SetData(new[] { Color.White });
            BoundingBox = new Rectangle((int)Position.X + 20, (int)Position.Y + 35, Width - 50, Height - 50);

            //Health
            Health = 5;
            MaxHealth = Health;

            AddWalkingAnimation();
            AddIdleAnimation();
            AddDeathAnimation();
            AddAttackAnimation();

            currentAnimation = _idleAnimation;
        }

        public override void Update(GameTime gameTime)
        {
            if(ImmunityTimer > 0)
            {
                ImmunityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            Move();
            if (_inputReader.ReadInput().X == 0 && _inputReader.ReadInput().Y == 0)
                currentAnimation = _idleAnimation;
            else
            {
                currentAnimation = _walkingAnimation;
                if (_inputReader.ReadInput().X == -1)
                {
                    horizontalFlip = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    horizontalFlip = SpriteEffects.None;
                }
            }

            if (Dead)
            {
                currentAnimation = _deathAnimation;
            }


            currentAnimation.Update(gameTime);
            BoundingBox = new Rectangle((int)Position.X + 20, (int)Position.Y + 35, Width - 50, Height - 50);
        }

        public override void Move()
        {
            Vector2 direction = _inputReader.ReadInput();

            // === Horizontal Movement ===
            if (direction.X != 0)
            {
                // Apply acceleration in the direction of input
                Speed = new Vector2(Speed.X + AccelerationMultiplier * direction.X, Speed.Y);
            }
            else
            {
                // Apply deceleration when no input is provided
                if (Math.Abs(Speed.X) > 0.2f)
                    Speed = new Vector2(Speed.X - 0.2f * Math.Sign(Speed.X), Speed.Y);
                else
                    Speed = new Vector2(0, Speed.Y); // Stop completely if below threshold
            }

            // Apply gravity
            if (_isJumping && _counter < 5)
            {
                Speed = new Vector2(Speed.X, 0.8f * Speed.Y);
                _counter++;
            }
            else
            {
                Speed = new Vector2(Speed.X, Speed.Y + Gravity);
                _counter = 0;
                _isJumping = false;
            }

            // Clamp speeds
            Speed = new Vector2(Math.Clamp(Speed.X, -4, MaxHorizontalSpeed), Math.Clamp(Speed.Y, -30, MaxVerticalSpeed));

            // === Horizontal Collision ===
            float horizontalMovement = Speed.X;
            Rectangle futureHorizontalBoundingBox = new Rectangle(
                (int)(BoundingBox.X + horizontalMovement),
                BoundingBox.Y,
                BoundingBox.Width,
                BoundingBox.Height
            );

            List<ICollidable> horizontalCollidables = CollisionHandler.CollidingWithObject(futureHorizontalBoundingBox);
            if (horizontalCollidables.Any(o => BoundingBox.Left <= o.BoundingBox.Right || BoundingBox.Right >= o.BoundingBox.Left))
            {
                // Stop horizontal movement only
                horizontalMovement = 0;
                Speed = new Vector2(horizontalMovement, Speed.Y);
            }

            // === Vertical Collision ===
            float verticalMovement = Speed.Y;
            Rectangle futureVerticalBoundingBox = new Rectangle(
                BoundingBox.X,
                (int)(BoundingBox.Y + verticalMovement),
                BoundingBox.Width,
                BoundingBox.Height
            );

            List<ICollidable> verticalCollidables = CollisionHandler.CollidingWithObject(futureVerticalBoundingBox);
            if (verticalCollidables.Any(o => BoundingBox.Bottom <= o.BoundingBox.Top) && !_isJumping)
            {
                _isJumping = false;
                _counter = 0;
                IsOnGround = true;
                verticalMovement = 0;
            }
            else if(verticalCollidables.Any(o => BoundingBox.Top >= o.BoundingBox.Bottom))
            {
                _isJumping = false;
                _counter = 0;
                IsOnGround = false;
                verticalMovement = Gravity;
            }

            //=== JUMP LOGIC ===
            if (direction.Y < 0 && IsOnGround)
            {
                IsOnGround = false;
                verticalMovement = -20f;
                Speed = new Vector2(Speed.X,verticalMovement);
                _isJumping = true;
            }

            Speed = new Vector2(Speed.X, verticalMovement);
            Position += Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Debug)
            {
                spriteBatch.Draw(boundingBoxTexture, BoundingBox, Color.Red);
            }
            spriteBatch.Draw(_heroTexture, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), horizontalFlip, 0f);
        }

        public void ChangeInput(IInputReader inputReader)
        {
            this._inputReader = inputReader;
        }

        public bool Intersects(IGameObject other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }

        #region Animations
        public void AddWalkingAnimation()
        {
            _walkingAnimation = new Animation();
            for (int i = 0; i < 6; i++)
            {
                _walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(Width * i, Height, Width, Height)));
            }
        }
        public void AddIdleAnimation()
        {
            _idleAnimation = new Animation();
            for (int i = 0; i < 9; i++)
            {
                _idleAnimation.AddFrame(new AnimationFrame(new Rectangle(Width * i, 0, Width, Height)));
            }
        }
        public void AddDeathAnimation()
        {
            _deathAnimation = new Animation();
            for (int i = 0; i < 22; i++)
            {
                _deathAnimation.AddFrame(new AnimationFrame(new Rectangle(Width * i, Height * 4, Width, Height)));
            }
        }
        public void AddAttackAnimation()
        {
            _attackAnimation = new Animation();
            for (int i = 0; i < 12; i++)
            {
                _attackAnimation.AddFrame(new AnimationFrame(new Rectangle(Width * i, Height * 3, Width, Height)));
            }
        }
        #endregion
    }
}
