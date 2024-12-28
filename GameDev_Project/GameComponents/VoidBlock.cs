using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.GameComponents
{
    public class VoidBlock : Block
    {
        public VoidBlock(int x, int y, Texture2D blockTexture) : base(x, y, blockTexture, Color.White)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(boundingBoxTexture, BoundingBox, new Rectangle(0, 3, Width, Height), Color.DarkSlateGray);
        }
    }
}
