using GameDev_Project.AnimationLogic;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Characters
{
    public class Hero : Character, IMovableObject
    {
        private bool debug = false;
        Texture2D heroTexture;
        Animation currentAnimation;
        Animation walkingAnimation;
        Animation idleAnimation;
        Animation deathAnimation;

        private Vector2 _pace;
        private IInputReader inputReader;
        private SpriteEffects horizontalFlip = SpriteEffects.None;

        public Hero(Texture2D texture, IInputReader inputReader, GraphicsDevice graphicsDevice)
        {
            Position = new Vector2(1, 1);
            _pace = new Vector2(1, 1);

            heroTexture = texture;
            this.inputReader = inputReader;
            Texture = new Texture2D(graphicsDevice,1,1);
            Texture.SetData(new[] { Color.White });
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 130, 60);

            AddWalkingAnimation();
            AddIdleAnimation();
            AddDeathAnimation();
        }

        public void Update(GameTime gameTime)
        {
            Move();
            if (inputReader.ReadInput().X == 0 && inputReader.ReadInput().Y == 0)
                currentAnimation = idleAnimation;
            if (inputReader.ReadInput().X == 1)
            {
                currentAnimation = walkingAnimation;
                horizontalFlip = SpriteEffects.None;
            }
            else if (inputReader.ReadInput().X == -1)
            {
                currentAnimation = walkingAnimation;
                horizontalFlip = SpriteEffects.FlipHorizontally;
            }

            currentAnimation.Update(gameTime);
            BoundingBox = new Rectangle((int)Position.X,(int)Position.Y, 130, 60);
        }

        private void Move()
        {
            var direction = inputReader.ReadInput();
            direction *= _pace;
            if (Position.X + direction.X > 0 && Position.X + direction.X < 800 - 160)
                Position = new Vector2(Position.X + direction.X, Position.Y);
            if (Position.Y + direction.Y > 0 && Position.Y + direction.Y < 480 - 96)
                Position = new Vector2(Position.X, Position.Y + direction.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (debug)
            {
                spriteBatch.Draw(Texture, BoundingBox, Color.Red);
            }
            spriteBatch.Draw(heroTexture, new Rectangle((int)Position.X,(int)Position.Y,160,96), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0,0),horizontalFlip,0f);
        }

        public void ChangeInput(IInputReader inputReader)
        {
            this.inputReader = inputReader;
        }

        public void AddWalkingAnimation()
        {
            walkingAnimation = new Animation();
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(0, 96, 160, 96)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(160, 96, 160, 96)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(320, 96, 160, 96)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(480, 96, 160, 96)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(640, 96, 160, 96)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(800, 96, 160, 96)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(960, 96, 160, 96)));
            walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(1120, 96, 160, 96)));
        }
        public void AddIdleAnimation()
        {
            idleAnimation = new Animation();
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 160, 96)));
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(160, 0, 160, 96)));
        }
        public void AddDeathAnimation()
        {
            deathAnimation = new Animation();
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(0, 384, 160, 96)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(160, 384, 160, 96)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(320, 384, 160, 96)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(480, 384, 160, 96)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(640, 384, 160, 96)));
            deathAnimation.AddFrame(new AnimationFrame(new Rectangle(800, 384, 160, 96)));
        }

        public bool Intersects(ICollide other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }
    }
}
