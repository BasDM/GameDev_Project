using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Interfaces
{
    public interface IObject
    {
        Vector2 position{ get; set; }
        public void Draw(SpriteBatch spriteBatch);
    }
}
