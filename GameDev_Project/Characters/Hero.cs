using GameDev_Project.AnimationLogic;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Characters
{
    public class Hero : IMovableObject
    {
        private bool debug = true;
        Texture2D heroTexture;
        Animation currentAnimation;
        Animation walkingAnimation;
        Animation idleAnimation;
        Animation deathAnimation;

        private Vector2 _pace;
        private IInputReader inputReader;
        private SpriteEffects horizontalFlip = SpriteEffects.None;

        public Rectangle boundingBox { get; set; }
        
        public Texture2D boundingBoxTexture { get; set; }
        public Vector2 position { get; set; } = new Vector2(0, 0);

        public Hero(Texture2D texture, IInputReader inputReader, GraphicsDevice graphicsDevice)
        {
            position = new Vector2(1, 1);
            _pace = new Vector2(1, 1);

            heroTexture = texture;
            this.inputReader = inputReader;
            boundingBoxTexture = new Texture2D(graphicsDevice,1,1);
            boundingBoxTexture.SetData(new[] { Color.White });
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 160, 96);

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
        }

        private void Move()
        {
            var direction = inputReader.ReadInput();
            direction *= _pace;
            if (position.X + direction.X > 0 && position.X + direction.X < 800 - 160)
                position = new Vector2(position.X + direction.X, position.Y);
            if (position.Y + direction.Y > 0 && position.Y + direction.Y < 480 - 96)
                position = new Vector2(position.X, position.Y + direction.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (debug)
            {
                spriteBatch.Draw(boundingBoxTexture, new Rectangle((int)position.X, (int)position.Y, 160, 96), Color.Red);
            }
            spriteBatch.Draw(heroTexture, new Rectangle((int)position.X,(int)position.Y,160,96), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0,0),horizontalFlip,0f);
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
            bool isIntersecting =  boundingBox.Intersects(other.boundingBox);
            return isIntersecting;
        }
    }
}
