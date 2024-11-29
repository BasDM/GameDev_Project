using GameDev_Project.AnimationLogic;
using GameDev_Project.Events;
using GameDev_Project.Extensions;
using GameDev_Project.Interfaces;
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
        Texture2D heroTexture;
        Animation currentAnimation;
        Animation walkingAnimation;
        Animation idleAnimation;
        Animation deathAnimation;
        Animation attackAnimation;

        private Vector2 _speed;
        private float MaxVerticalSpeed = 5;
        private float MaxHorizontalSpeed = 5;

        private Vector2 _acceleration;
        private float AccelerationMultiplier=5;
        private float MaxAcceleration = 4;

        private IInputReader inputReader;
        private SpriteEffects horizontalFlip = SpriteEffects.None;

        //deceleration
        private const float GroundDeceleration = 0.48f;
        private const float AirDeceleration = 0.58f;

        //Collision
        private float previousBottom;

        private int width = 80;
        private int height = 80;
        private bool isOnGround = false;
        #endregion
        private Vector2 Limit(Vector2 v, float min, float max)
        {
            float length = v.Length();

            if (length > max)
            {
                float ratio = max / length;
                v.X *= ratio;
                v.Y *= ratio;
            }
            else if (length < min && length > 0)
            {
                float ratio = min / length;
                v.X *= ratio;
                v.Y *= ratio;
            }

            return v;
        }
        public Hero(Texture2D texture, IInputReader inputReader, GraphicsDevice graphicsDevice)
        {
            Position = new Vector2(0,20);
            _speed = new Vector2(0, 0);
            _acceleration = new Vector2(0.9f, 0.9f);

            heroTexture = texture;
            this.inputReader = inputReader;
            Texture = new Texture2D(graphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.White });
            BoundingBox = new Rectangle((int)Position.X + 20, (int)Position.Y + 35, width - 50, height - 50);

            //Health
            health = 5;
            maxHealth = health;

            AddWalkingAnimation();
            AddIdleAnimation();
            AddDeathAnimation();
            AddAttackAnimation();
        }

        public override void Update(GameTime gameTime)
        {
            Move();
            if (inputReader.ReadInput().X == 0 && inputReader.ReadInput().Y == 0)
                currentAnimation = idleAnimation;
            else
            {
                currentAnimation = walkingAnimation;
                if (inputReader.ReadInput().X == -1)
                {
                    horizontalFlip = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    horizontalFlip = SpriteEffects.None;
                }
            }

            currentAnimation.Update(gameTime);
            BoundingBox = new Rectangle((int)Position.X + 20, (int)Position.Y + 35, width - 50, height - 50);
        }

        public void Move()
        {
            Vector2 direction = inputReader.ReadInput();

            // === Horizontal Movement ===
            if (direction.X != 0)
            {
                // Apply acceleration in the direction of input
                _speed = new Vector2(_speed.X + 0.9f * direction.X, _speed.Y);
            }
            else
            {
                // Apply deceleration when no input is provided
                if (Math.Abs(_speed.X) > 0.2f)
                    _speed = new Vector2(_speed.X - 0.2f * Math.Sign(_speed.X), _speed.Y);
                else
                    _speed = new Vector2(0, _speed.Y); // Stop completely if below threshold
            }

            // Apply gravity
            _speed = new Vector2(_speed.X, _speed.Y + 0.1f);

            // Clamp speeds
            _speed = new Vector2(Math.Clamp(_speed.X, -4, 4), Math.Clamp(_speed.Y, -30, 80));

            // === Horizontal Collision ===
            float horizontalMovement = _speed.X;
            Rectangle futureHorizontalBoundingBox = new Rectangle(
                (int)(BoundingBox.X + horizontalMovement),
                BoundingBox.Y,
                BoundingBox.Width,
                BoundingBox.Height
            );

            List<ICollidable> horizontalCollidables = CollisionHandler.CollidingWithObject(futureHorizontalBoundingBox);
            if (horizontalCollidables.Any(o => BoundingBox.Left >= o.BoundingBox.Right || BoundingBox.Right <= o.BoundingBox.Left))
            {
                // Stop horizontal movement only
                horizontalMovement = 0;
                _speed = new Vector2(0, _speed.Y);
            }
            else
            {
                Position += new Vector2(horizontalMovement, 0);
            }

            // === Vertical Collision ===
            float verticalMovement = _speed.Y;
            Rectangle futureVerticalBoundingBox = new Rectangle(
                BoundingBox.X,
                (int)(BoundingBox.Y + verticalMovement),
                BoundingBox.Width,
                BoundingBox.Height
            );

            List<ICollidable> verticalCollidables = CollisionHandler.CollidingWithObject(futureVerticalBoundingBox);
            if (verticalCollidables.Count > 0)
            {
                // Stop at the ground if moving down
                if (verticalMovement > 0) // Moving down
                {
                    verticalMovement = 0;
                    isOnGround = true;
                }
                else // Moving up
                {
                    verticalMovement = 0;
                    isOnGround = false;
                }

                _speed = new Vector2(_speed.X, 0);
            }
            else
            {
                Position += new Vector2(0, verticalMovement);
                isOnGround = false;
            }

            // === Jump Handling ===
            if (direction.Y < 0 && isOnGround)
            {
                _speed = new Vector2(_speed.X, -5f); // Jump
                isOnGround = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (debug)
            {
                spriteBatch.Draw(Texture, BoundingBox, Color.Red);
            }
            spriteBatch.Draw(heroTexture, new Rectangle((int)Position.X, (int)Position.Y, width, height), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), horizontalFlip, 0f);
        }

        public override void HaltMovement(ICollidable other)
        {
            Vector2 collisionDirection = Position - other.Position;
            collisionDirection = Limit(collisionDirection, -1f, 1f);

            Vector2 separation = Vector2.Zero;

            if (Math.Abs(collisionDirection.X) < (width + other.width) / 2)
            {
                separation.X = -_speed.X;
                _speed.X = 0;
                _acceleration.X = 0;
            }

            if (Math.Abs(collisionDirection.Y) < (height + other.height) / 2)
            {
                separation.Y = -_speed.Y;
                _speed.Y = 0;
                _acceleration.Y = 0;
                if (collisionDirection.Y > -(height + other.height) / 2)
                {
                    isOnGround = true;
                }
            }

            Position += separation;
        }

        public void ChangeInput(IInputReader inputReader)
        {
            this.inputReader = inputReader;
        }

        public bool Intersects(IGameObject other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }

        #region Animations
        public void AddWalkingAnimation()
        {
            walkingAnimation = new Animation();
            for (int i = 0; i < 6; i++)
            {
                walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(width * i, height, width, height)));
            }
        }
        public void AddIdleAnimation()
        {
            idleAnimation = new Animation();
            for (int i = 0; i < 9; i++)
            {
                idleAnimation.AddFrame(new AnimationFrame(new Rectangle(width * i, 0, width, height)));
            }
        }
        public void AddDeathAnimation()
        {
            deathAnimation = new Animation();
            for (int i = 0; i < 22; i++)
            {
                deathAnimation.AddFrame(new AnimationFrame(new Rectangle(width * i, height * 4, width, height)));
            }
        }
        public void AddAttackAnimation()
        {
            attackAnimation = new Animation();
            for (int i = 0; i < 12; i++)
            {
                attackAnimation.AddFrame(new AnimationFrame(new Rectangle(width * i, height * 3, width, height)));
            }
        }
        #endregion
    }
}
