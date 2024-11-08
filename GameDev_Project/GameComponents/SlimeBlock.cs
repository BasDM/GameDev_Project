using GameDev_Project.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.GameComponents
{
    public class SlimeBlock : Block
    {
        public SlimeBlock(int x, int y, GraphicsDevice graphics) : base(x,y,graphics)
        {
            CollideWithEvent = new SlowEvent();
        }
    }
}
