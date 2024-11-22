using GameDev_Project.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameDev_Project.Factories;
using Microsoft.Xna.Framework.Content;

namespace GameDev_Project.Factories
{
    public class BlockFactory
    {
        public static Block CreateBlock( string type, Texture2D texture ,int x, int y)
        {
            Block newBlock = null;
            type = type.ToUpper();
            if(type == "NORMAL")
            {
                newBlock = new Block(x,y);
            }
            if (type == "TRAP")
            {
                newBlock = new TrapBlock(x,y);
            }
            if (type == "SLIME")
            {
                newBlock = new SlimeBlock(x, y);
            }
            return newBlock;
        }

        public static Block CreateBlockWithInt(int type, int x, int y) {
            Block newBlock = null;
            switch (type)
            {
                case 0:
                    break;
                case 1:
                    newBlock = new Block(x*100, y*80);
                    break;
                case 2:
                    newBlock = new SlimeBlock(x*100, y*80);
                    break;
                default:
                    break;
            }
            return newBlock;
        }
    }
}
