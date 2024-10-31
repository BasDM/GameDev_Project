using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Interfaces
{
    public interface ICollide : IObject
    {
        Rectangle boundingBox { get; set; }
        Texture2D boundingBoxTexture { get; set; }
        bool Intersects(ICollide other);
    }
}
