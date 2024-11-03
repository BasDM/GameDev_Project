using GameDev_Project.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.GameComponents
{
    public class TrapBlock : Block
    {
        public int Damage { get; set; }
        public TrapBlock(int x, int y, GraphicsDevice graphics) : base(x, y, graphics)
        {
            BoundingBox = new Rectangle(x, y, 10, 10);
            Passable = true;
            Color = Color.Black;
            Texture = new Texture2D(graphics, 1, 1);
            Damage = 10;
            CollideWithEvent = new DamageEvent(Damage);
        }
    }
}
