using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
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
        public List<Heart> hearts { get; set; }

        public UserInterface(int amountOfHealth, Texture2D heartTexture, int heartWidth, int heartHeight, Vector2 heartPosition, Rectangle heartBoundingBox)
        {
            hearts = new List<Heart>();
            for (int i = 0; i < amountOfHealth; i++)
            {
                hearts.Add(new Heart(heartTexture, heartWidth, heartHeight, heartPosition, heartBoundingBox));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var heart in hearts)
            {
                heart.Draw(spriteBatch);
                heart.Position = new Vector2(heart.width, heart.Position.Y);
            }
        }
    }
}
