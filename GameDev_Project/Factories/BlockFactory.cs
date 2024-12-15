using GameDev_Project.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameDev_Project.Factories;
using Microsoft.Xna.Framework.Content;

namespace GameDev_Project.Factories
{
    public class BlockFactory
    {
        public static Block CreateBlock(Texture2D texture, int x, int y)
        {
            Block newBlock = null;
            newBlock = new Block(x,y,texture);
            return newBlock;
        }

        public static Block CreateBlockWithInt(int type, int x, int y,Texture2D texture) {
            Block newBlock = null;
            switch (type)
            {
                case 0:
                    break;
                case 1:
                    newBlock = new Block(x*50, y*50, texture);
                    break;
                case 9:
                    newBlock = new VoidBlock(x * 50, y * 50, texture);
                    break;
                default:
                    break;
            }
            return newBlock;
        }
    }
}
