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
        Animation animation;
        private Vector2 _position;
        private Vector2 _pace;
        private IInputReader inputReader;

        public Hero(Texture2D texture, IInputReader inputReader)
        {
            this.heroTexture = texture;
            this.inputReader = inputReader;
            animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(160, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(320, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(480, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(640, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(800, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(960, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(1120, 96, 160, 96)));

            _position = new Vector2(1, 1);
            _pace = new Vector2(1, 1);
        }

        public void Update(GameTime gameTime)
        {
            Move();
            animation.Update(gameTime);
        }

        private void Move()
        {
            var direction = inputReader.ReadInput();
            direction *= _pace;
            _position += direction;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, _position, animation.CurrentFrame.SourceRectangle, Color.White);
        }

        public void ChangeInput(IInputReader inputReader)
        {
            this.inputReader = inputReader;
        }
    }
}
