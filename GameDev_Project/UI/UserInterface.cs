using GameDev_Project.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.UI
{
    public class UserInterface
    {
        private Hero hero;
        private int heartWidth;
        private int heartHeight;
        private Vector2 heartPosition;
        private Texture2D heartTexture;
        private SpriteFont score;

        public UserInterface(Hero _hero, ContentManager _content, int _heartWidth, int _heartHeight, Vector2 _heartPosition)
        {
            hero = _hero;
            heartTexture = _content.Load<Texture2D>("heart_16x16");
            score = _content.Load<SpriteFont>(@"Fonts\ScoreFont");
            heartWidth = _heartWidth;
            heartHeight = _heartHeight;
            heartPosition = _heartPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < hero.Health; i++)
            {
                spriteBatch.Draw(heartTexture, new Vector2(heartPosition.X + i * heartWidth, heartPosition.Y), Color.White);
            }
            spriteBatch.DrawString(score, $"Score: {hero._coinsCollected}", new Vector2(heartPosition.X + 700 , heartPosition.Y ), Color.White); // Add this line
        }
    }
}