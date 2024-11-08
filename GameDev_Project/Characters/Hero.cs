using GameDev_Project.AnimationLogic;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
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

        private Vector2 _speed;
        private Vector2 _acceleration;
        private IInputReader inputReader;
        private SpriteEffects horizontalFlip = SpriteEffects.None;

        private int width = 40;
        private int height = 30;
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
            Position = new Vector2(1, 1);
            _speed = new Vector2(0, 0);
            _acceleration = new Vector2(0.1f, 0.1f);

            heroTexture = texture;
            this.inputReader = inputReader;
            Texture = new Texture2D(graphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.White });
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, width, height);

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
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, width, height);
        }

        private void Move()
        {
            var direction = inputReader.ReadInput();

            //slow down when no input
            if (direction.X == 0)
            {
                _acceleration.X = 0;
                //friction
                _speed.X *= 0.9f;
            }

            if (direction.Y == 0)
            {
                _acceleration.Y = 0;
                //friction
                _speed.Y *= 0.9f;

            }

            if (_speed.Length() < 0.01f)
                _speed = Vector2.Zero;



            _acceleration += direction / 600;
            Limit(_acceleration, -0.07f, 0.07f);

            _speed += _acceleration;
            Limit(_speed, -0.1f, 0.1f);


            var nextPositionX = Position.X + _speed.X;
            var nextPositionY = Position.Y + _speed.Y;


            if (nextPositionX < 0 || nextPositionX > 800 - 160)
            {
                _speed.X = 0;
            }

            if (nextPositionY < 0 || nextPositionY > 480 - 96)
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
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(0, height, width, height)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(width, height, width, height)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(width * 2, height, width, height)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(width * 3, height, width, height)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(width * 4, height, width, height)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(width * 5, height, width, height)));
        }
        public void AddIdleAnimation()
        {
            idleAnimation = new Animation();
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(0, 0, width, height)));
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(width, 0, width, height)));
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(width*2, 0, width, height)));
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(width*3, 0, width, height)));
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(width*4, 0, width, height)));
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(width*5, 0, width, height)));
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(width*6, 0, width, height)));
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(width*7, 0, width, height)));
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(width*8, 0, width, height)));
        }
        public void AddDeathAnimation()
        {
            deathAnimation = new Animation();
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(0, height*4, width, height)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(width, height*4, width, height)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(width * 2, height*4, width, height)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(width * 3, height*4, width, height)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(width * 4, height*4, width, height)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(width * 5, height*4, width, height)));
        }

        public bool Intersects(IGameObject other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }

        public override void HandleCollision(ICollidable other)
        {
            Vector2 collisionDirection = Position - other.Position;
            collisionDirection = Limit(collisionDirection, -1f, 1f);

            //Stop if collision in X direction
            if (Math.Abs(collisionDirection.X) > 0.1f)
            {
                _speed.X *= 0;
                _acceleration.X = 0;
            }

            //Same thing but in Y direction
            if (Math.Abs(collisionDirection.Y) > 0.1f)
            {
                _speed.Y *= 0;
                _acceleration.Y = 0;
            }
        }
    }
}
