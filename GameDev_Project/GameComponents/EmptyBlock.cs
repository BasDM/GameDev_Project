using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameDev_Project.GameComponents
{
    public class EmptyBlock : Block
    {
        public EmptyBlock(int x, int y, GraphicsDevice graphics) : base(x, y, graphics)
        {
            Color = Color.CornflowerBlue;
            Texture.SetData(new[] { Color.White });
        }
    }
}
