using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Interfaces
{
    public interface ICollide : IObject
    {
        Rectangle BoundingBox { get; set; }
        Texture2D Texture { get; set; }
        bool Intersects(ICollide other);
    }
}
