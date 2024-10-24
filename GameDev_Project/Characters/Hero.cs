using System;
using System.Diagnostics;
using GameDev_Project.AnimationLogic;
using GameDev_Project.Input;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDev_Project.Characters
{

    public class Hero : IGameObject
    {
        Texture2D heroTexture;
        Animation currentAnimation;
        Animation walkingAnimation;
        Animation idleAnimation;
        private Vector2 _position;
        private Vector2 _pace;
        private IInputReader inputReader;
        private SpriteEffects horizontalFlip = SpriteEffects.None;

        public Hero(Texture2D texture, IInputReader inputReader)
        {
            this.heroTexture = texture;
            this.inputReader = inputReader;
            addWalkingAnimation();
            addIdleAnimation();

            _position = new Vector2(1, 1);
            _pace = new Vector2(1, 1);
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

            walkingAnimation.Update(gameTime);
        }

        private void Move()
        {
            var direction = inputReader.ReadInput();
            direction *= _pace;
            _position += direction;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, new Rectangle((int)_position.X,(int)_position.Y,160,96), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0,0),horizontalFlip,0f);
        }

        public void ChangeInput(IInputReader inputReader)
        {
            this.inputReader = inputReader;
        }

        public void addWalkingAnimation()
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
        public void addIdleAnimation()
        {
            idleAnimation = new Animation();
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 160, 96)));
            idleAnimation.AddFrame(new AnimationFrame(new Rectangle(160, 0, 160, 96)));
        }
    }
}
