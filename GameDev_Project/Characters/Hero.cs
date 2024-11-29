using GameDev_Project.AnimationLogic;
using GameDev_Project.Events;
using GameDev_Project.GameComponents;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Xml.Linq;

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
        private Vector2 _acceleration;
        private IInputReader inputReader;
        private SpriteEffects horizontalFlip = SpriteEffects.None;

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
                {
                    _speed = new Vector2(_speed.X - 0.2f * Math.Sign(_speed.X), _speed.Y);
                }
                else
                {
                    _speed = new Vector2(0, _speed.Y); // Stop completely if below threshold
                }
            }

            // Apply gravity
            _speed = new Vector2(_speed.X, _speed.Y + 0.1f);

            // Clamp vertical speed
            _speed = new Vector2(_speed.X, Math.Clamp(_speed.Y, -30, 80));

            // Clamp horizontal speed
            _speed = new Vector2(Math.Clamp(_speed.X, -4, 4), _speed.Y);

            // Horizontal collision and position update
            float horizontalMovement = _speed.X;
            Rectangle futureHorizontalBoundingbox = new Rectangle(
                (int)(BoundingBox.X + horizontalMovement),
                BoundingBox.Y,
                BoundingBox.Width,
                BoundingBox.Height
            );

            if (CollisionHandler.CollidingWithObject(futureHorizontalBoundingbox) == null)
            {
                Position += new Vector2(horizontalMovement, 0);
            }
            else
            {
                _speed = new Vector2(0, _speed.Y); // Stop horizontal movement on collision
            }

            // === Vertical Movement ===
            if (direction.Y < 0 && isOnGround)
            {
                _speed = new Vector2(_speed.X, -5f); // Jump
                isOnGround = false;
            }
            else if(direction.Y < 0)
            {
                isOnGround = false;
            }

            // Vertical collision and position update
            float verticalMovement = _speed.Y;
            Rectangle futureVerticalBoundingbox = new Rectangle(
                BoundingBox.X,
                (int)(BoundingBox.Y + verticalMovement),
                BoundingBox.Width,
                BoundingBox.Height
            );

            var futureVerticalHit = CollisionHandler.CollidingWithObject(futureVerticalBoundingbox);
            if (futureVerticalHit == null)
            {
                Position += new Vector2(0, verticalMovement);
            }
            else if((BoundingBox.Bottom > futureVerticalHit.BoundingBox.Top))
            {
                isOnGround = true; // Landed on ground
                _speed = new Vector2(_speed.X, 0); // Stop vertical movement
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
