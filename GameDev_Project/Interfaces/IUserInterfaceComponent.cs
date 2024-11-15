using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.Interfaces
{
    public interface IUserInterfaceComponent
    {
        Texture2D Texture { get; set; }
        int width { get; set; }
        int height { get; set; }
        Vector2 Position { get; set; }
        Rectangle BoundingBox { get; set; }

        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
    }
}
