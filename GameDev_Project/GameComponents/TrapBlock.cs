using GameDev_Project.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.GameComponents
{
    public class TrapBlock : Block
    {
        public int Damage { get; set; }
        public TrapBlock(int x, int y) : base(x, y)
        {
            
        }
    }
}
