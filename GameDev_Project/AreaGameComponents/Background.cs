using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.AreaGameComponents
{
    public class Background
    {
        public void Draw(SpriteBatch spriteBatch, Rectangle screen)
        {
            spriteBatch.Draw(Game1.backgroundTexture, screen, Color.White);
        }
    }
}
