using GameDev_Project.AnimationLogic;
using GameDev_Project.GameComponents;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.InteropServices;

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
            Position = new Vector2(20, 80);
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
            var direction = inputReader.ReadInput();

            // Apply gravity
            _acceleration.Y = 0.1f;  // Gravity value

            // Check if the hero is on the ground to allow jumping
            if (isOnGround && direction.Y < 0)
            {
                _speed.Y = -3.0f;  // Jump strength
                isOnGround = false;
            }

            // Allow horizontal movement
            _acceleration.X = direction.X * 0.1f;

            // Slow down when no horizontal input
            if (direction.X == 0)
            {
                _speed.X *= 0.8f;  // Friction
            }

            if (_speed.Length() < 0.01f)
                _speed = Vector2.Zero;

            _speed += _acceleration;
            Limit(_speed, -0.1f, 0.1f);

            var nextPositionX = Position.X + _speed.X;
            var nextPositionY = Position.Y + _speed.Y;

            // Collision with screen boundaries
            if (nextPositionX < 0 || nextPositionX > (Game1.screen.Width - width))
            {
                _speed.X = 0;
            }

            if (nextPositionY < 0 || nextPositionY > (Game1.screen.Height - height))
            {
                _speed.Y = 0;
            }

            Position += _speed;

            // Reset isOnGround if hero is on the ground
            if (Position.Y >= Game1.screen.Height - height)
            {
                isOnGround = true;
                _speed.Y = 0;
                _acceleration.Y = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (debug)
            {
                spriteBatch.Draw(Texture, BoundingBox, Color.Red);
            }
            spriteBatch.Draw(heroTexture, new Rectangle((int)Position.X , (int)Position.Y, width, height), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), horizontalFlip, 0f);
        }


        public override void HaltMovement(ICollidable other)
        {
            Vector2 collisionDirection = Position - other.Position;
            collisionDirection = Limit(collisionDirection, -1f, 1f);

            Vector2 separation = Vector2.Zero;

            if (Math.Abs(collisionDirection.X) >= 0.1f)
            {
                separation.X = -_speed.X;
                _speed.X = 0;
                _acceleration.X = 0;
            }

            if (Math.Abs(collisionDirection.Y) >= 0.1f)
            {
                separation.Y = -_speed.Y;
                _speed.Y = 0;
                _acceleration.Y = 0;
                isOnGround = true;  // Set isOnGround to true when colliding from the top
            }

            Position += separation;
        }

        public override void HandleCollision(ICollidable other)
        {
            if(other is Block)
            {
                HaltMovement(other);
            }
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
                deathAnimation.AddFrame(new AnimationFrame(new Rectangle(width * i, height*4, width, height)));
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
