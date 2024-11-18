using GameDev_Project.Characters;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.UI
{
    public class UserInterface
    {
        private Hero hero;
        private ContentManager content;
        private int heartWidth;
        private int heartHeight;
        private Vector2 heartPosition;
        private Texture2D heartTexture;

        public UserInterface(Hero _hero, ContentManager _content, int _heartWidth, int _heartHeight, Vector2 _heartPosition)
        {
            hero = _hero;
            heartTexture = _content.Load<Texture2D>("heart_16x16");
            heartWidth = _heartWidth;
            heartHeight = _heartHeight;
            heartPosition = _heartPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < hero.health; i++)
            {
                UIElement heart = new UIElement(heartTexture, heartWidth, heartHeight, new Vector2(heartPosition.X + i * heartWidth, heartPosition.Y));
                heart.Draw(spriteBatch);
            }
        }
    }
}
