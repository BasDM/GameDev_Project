using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameDev_Project.GameComponents
{
    public class Block : ICollide
    {
        public Rectangle boundingBox { get; set; }
        public Texture2D boundingBoxTexture { get; set; }
        public Vector2 position { get; set; }

        public Block(GraphicsDevice graphicsDevice, Vector2 position)
        {
            this.position = position;

            this.boundingBoxTexture = new Texture2D(graphicsDevice, 1, 1);
            this.boundingBoxTexture.SetData(new[] { Color.White });
            this.boundingBox = new Rectangle((int)position.X, (int)position.Y, 160, 96);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(boundingBoxTexture, boundingBox, Color.White);
        }


        public bool Intersects(ICollide other)
        {
            return boundingBox.Intersects(other.boundingBox);
        }
    }
}
