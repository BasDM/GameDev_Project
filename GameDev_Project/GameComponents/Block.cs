using GameDev_Project.Interfaces;
using GameDev_Project.Scenes;
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

        public Block(int x, int y, Texture2D blockTexture)
        {
            Width = 50;
            Height = 50;
            BoundingBox = new Rectangle(x, y, Width, Height);
            boundingBoxTexture = blockTexture;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(this is VoidBlock)
            {
                this.Draw(spriteBatch);
            }
            spriteBatch.Draw(boundingBoxTexture, BoundingBox, new Rectangle(0, 3, Width, Height), Color.White);
        }

        public bool Intersects(ICollidable other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }
    }
}
