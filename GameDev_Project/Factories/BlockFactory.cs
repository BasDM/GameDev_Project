using GameDev_Project.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameDev_Project.Factories;
using Microsoft.Xna.Framework.Content;

namespace GameDev_Project.Factories
{
    public class BlockFactory
    {
        public static Block CreateBlockWithInt(int type, int x, int y,Texture2D texture, Color color) {
            Block newBlock = null;
            switch (type)
            {
                case 0:
                    break;
                case 1:
                    newBlock = new Block(x*50, y*50, texture, color);
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
