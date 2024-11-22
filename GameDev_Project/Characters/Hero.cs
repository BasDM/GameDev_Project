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

        private void Move()
        {
            var direction = inputReader.ReadInput();

            //slow down when no input
            if (direction.X == 0)
            {
                _acceleration.X = 0;
                //friction
                _speed.X *= 0.8f;
            }

            if (direction.Y == 0)
            {
                _acceleration.Y = 0;
                //friction
                _speed.Y *= 0.8f;

            }

            if (_speed.Length() < 0.01f)
                _speed = Vector2.Zero;

            //TODO add gravity


            _acceleration += direction / 900;
            Limit(_acceleration, -0.07f, 0.07f);

            _speed += _acceleration;
            Limit(_speed, -0.1f, 0.1f);


            var nextPositionX = Position.X + _speed.X;
            var nextPositionY = Position.Y + _speed.Y;


            if (nextPositionX < 0 || nextPositionX > (Game1.screen.Width - width))
            {
                _speed.X = 0;
            }

            if (nextPositionY < 0 || nextPositionY > (Game1.screen.Height - height))
            {
                _speed.Y = 0;
            }

            Position += _speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (debug)
            {
                spriteBatch.Draw(Texture, BoundingBox, Color.Red);
            }
            spriteBatch.Draw(heroTexture, new Rectangle((int)Position.X , (int)Position.Y, width, height), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), horizontalFlip, 0f);
        }

        public void ChangeInput(IInputReader inputReader)
        {
            this.inputReader = inputReader;
        }

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

        public bool Intersects(IGameObject other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }

        public override void HaltMovement(ICollidable other)
        {
            Vector2 collisionDirection = Position - other.Position;
            collisionDirection = Limit(collisionDirection, -1f, 1f);

            //How much you need to be pushed back when colliding to not clip into the wall
            Vector2 seperation = Vector2.Zero;
            

            //Stop if collision in X direction
            if (Math.Abs(collisionDirection.X) >= 0.1f)
            {
                seperation.X = -_speed.X;
                _speed.X *= 0;
                _acceleration.X = 0;
            }

            //Same thing but in Y direction
            if (Math.Abs(collisionDirection.Y) >= 0.1f)
            {
                seperation.Y = -_speed.Y;
                _speed.Y *= 0;
                _acceleration.Y = 0;
            }

            Position += seperation;

        }

        public override void HandleCollision(ICollidable other)
        {
            if(other is Block)
            {
                HaltMovement(other);
            }
        }
    }
}
