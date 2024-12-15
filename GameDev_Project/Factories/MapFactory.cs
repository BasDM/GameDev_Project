using GameDev_Project.Events;
using GameDev_Project.GameComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.Factories
{
    public class MapFactory
    {
        public static List<Block> CreateBlocks(int[,] gameBoard)
        {
            List<Block> blocks = new List<Block>();
            for (int l = 0; l < gameBoard.GetLength(0); l++)
            {
                for (int k = 0; k < gameBoard.GetLength(1); k++)
                {
                    int val = gameBoard[l, k];
                    if (val != 0)
                    {
                        blocks.Add(BlockFactory.CreateBlockWithInt(val, k, l));
                    }
                }
            }

            foreach (var block in blocks)
            {
                CollisionHandler.AddCollidable(block);
            }
            return blocks;
        }
    }
}
