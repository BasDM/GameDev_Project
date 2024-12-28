using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.GameComponents
{
    public class Block : IAreaGameComponent
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public Rectangle BoundingBox { get; set; }
        public Texture2D boundingBoxTexture { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }

        public Block(int x, int y, Texture2D blockTexture, Color color)
        {
            Width = 50;
            Height = 50;
            Color = color;
            BoundingBox = new Rectangle(x, y, Width, Height);
            boundingBoxTexture = blockTexture;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(boundingBoxTexture, BoundingBox, new Rectangle(0, 3, Width, Height), Color);
        }

        public bool Intersects(ICollidable other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }
    }
}
