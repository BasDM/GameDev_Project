using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.GameComponents
{
    public class CoinBlock : Block
    {
        public bool collected = false;
        public CoinBlock(int x, int y, Texture2D blockTexture, Color color) : base(x, y, blockTexture, color)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(boundingBoxTexture, new Rectangle(BoundingBox.Center.X, BoundingBox.Center.Y, 15, 15), Color.White);
        }
    }
}
