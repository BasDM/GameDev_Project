using GameDev_Project.AnimationLogic;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Characters
{
    public class Hero : Character, IGameObject
    {
        Texture2D heroTexture;
        Animation currentAnimation;
        Animation walkingAnimation;
        Animation idleAnimation;
        Animation deathAnimation;

        private Vector2 _pace;
        private IInputReader inputReader;
        private SpriteEffects horizontalFlip = SpriteEffects.None;

        private int width = 160;
        private int height = 96;

        public Hero(Texture2D texture, IInputReader inputReader, GraphicsDevice graphicsDevice)
        {
            Position = new Vector2(1, 1);
            _pace = new Vector2(1, 1);

            heroTexture = texture;
            this.inputReader = inputReader;
            Texture = new Texture2D(graphicsDevice,1,1);
            Texture.SetData(new[] { Color.White });
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            AddWalkingAnimation();
            AddIdleAnimation();
            AddDeathAnimation();
        }

        public void Update(GameTime gameTime)
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
            BoundingBox = new Rectangle((int)Position.X,(int)Position.Y, width, height);
        }

        private void Move()
        {
            var direction = inputReader.ReadInput();
            var nextPositionX = Position.X + direction.X;
            var nextPositionY = Position.Y + direction.Y;

            //if (Check intersect) { };

            direction *= _pace;
            if (nextPositionX > 0 && nextPositionX < 800 - 160)
                Position = new Vector2(Position.X + direction.X, Position.Y);
            if (nextPositionY > 0 && nextPositionY < 480 - 96)
                Position = new Vector2(Position.X, Position.Y + direction.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (debug)
            {
                spriteBatch.Draw(Texture, BoundingBox, Color.Red);
            }
            spriteBatch.Draw(heroTexture, new Rectangle((int)Position.X,(int)Position.Y,width,height), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0,0),horizontalFlip,0f);
        }

        public void ChangeInput(IInputReader inputReader)
        {
            this.inputReader = inputReader;
        }

        public void AddWalkingAnimation()
        {
            walkingAnimation = new Animation();
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(0, 96, width, height)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(160, 96, width, height)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(320, 96, width, height)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(480, 96, width, height)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(640, 96, width, height)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(800, 96, width, height)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(960, 96, width, height)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(1120, 96, width, height)));
        }
        public void AddIdleAnimation()
        {
            idleAnimation = new Animation();
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(0, 0, width, height)));
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(160, 0, width, height)));
        }
        public void AddDeathAnimation()
        {
            deathAnimation = new Animation();
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(0, 384, width, height)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(160, 384, width, height)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(320, 384, width, height)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(480, 384, width, height)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(640, 384, width, height)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(800, 384, width, height)));
        }

        public bool Intersects(IGameObject other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }
    }
}
