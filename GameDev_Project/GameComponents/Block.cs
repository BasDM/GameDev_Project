using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.GameComponents
{
    public class Block : IAreaGameComponent
    {
        public int height { get; set; }
        public int width { get; set; }
        public Rectangle BoundingBox { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }

        public Block(int x, int y)
        {
            width = 50;
            height = 50;
            BoundingBox = new Rectangle(x, y, width, height);
            Color = Color.White;
            Texture = Game1.blockTexture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, BoundingBox, new Rectangle(0, 3, width, height), Color.White);
        }

        public bool Intersects(ICollidable other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }
    }
}
