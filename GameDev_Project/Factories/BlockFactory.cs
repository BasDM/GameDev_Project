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

        public static Block CreateBlockWithInt(int type, int x, int y, GraphicsDevice graphics) {
            Block newBlock = null;
            switch (type)
            {
                case 0:
                    newBlock = new EmptyBlock(x*100, y*50, graphics);
                    break;
                case 1:
                    newBlock = new Block(x*100, y*50, graphics);
                    break;
                case 2:
                    newBlock = new SlimeBlock(x*100, y*50, graphics);
                    break;
                default:
                    break;
            }
            return newBlock;
        }
    }
}
