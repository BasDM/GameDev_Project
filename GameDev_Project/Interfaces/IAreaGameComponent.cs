using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Interfaces
{
    public interface IAreaGameComponent:ICollidable
    {
        Texture2D boundingBoxTexture { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}
