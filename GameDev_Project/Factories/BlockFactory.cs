using GameDev_Project.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameDev_Project.Factories;

namespace GameDev_Project.Factories
{
    public class BlockFactory
    {
        public static Block CreateBlock( string type, int x, int y, GraphicsDevice graphics)
        {
            Block newBlock = null;
            type = type.ToUpper();
            if(type == "NORMAL")
            {
                newBlock = new Block(x,y,graphics);
            }
            if (type == "TRAP")
            {
                newBlock = new TrapBlock(x,y,graphics);
            }
            if (type == "SLIME")
            {
                newBlock = new SlimeBlock(x, y,graphics);
            }
            return newBlock;
        }
    }
}
