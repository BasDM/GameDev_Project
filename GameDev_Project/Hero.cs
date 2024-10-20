using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameDev_Project.Interfaces;
using GameDev_Project.AnimationLogic;

namespace GameDev_Project
{

    public class Hero:IGameObject
    {
        Texture2D heroTexture;
        Animation animation;

        public Hero(Texture2D texture)
        {
            heroTexture = texture;
            animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(160, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(320, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(480, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(640, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(800, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(960, 96, 160, 96)));
            animation.AddFrame(new AnimationFrame(new Rectangle(1120, 96, 160, 96)));
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, new Vector2(0, 0),animation.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}
