using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Interfaces
{
    public interface IAreaGameComponent:ICollidable
    {
        Texture2D Texture { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}
