using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Interfaces
{
    public interface IAreaGameComponent
    {
        Vector2 Position { get; set; }
        Rectangle BoundingBox { get; set; }
        Texture2D Texture { get; set; }
        void Draw(SpriteBatch spriteBatch);
        bool Intersects(IGameObject other);
    }
}
