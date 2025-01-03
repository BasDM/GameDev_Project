﻿using GameDev_Project.Events;
using GameDev_Project.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameDev_Project.Factories
{
    public class MapFactory
    {
        public static List<Block> CreateBlocks(int[,] gameBoard, Texture2D blockTexture, Texture2D coinTexture, Color color)
        {
            List<Block> blocks = new List<Block>();
            for (int l = 0; l < gameBoard.GetLength(0); l++)
            {
                for (int k = 0; k < gameBoard.GetLength(1); k++)
                {
                    int val = gameBoard[l, k];
                    if (val != 0)
                    {
                        blocks.Add(BlockFactory.CreateBlockWithInt(val, k, l,blockTexture,coinTexture, color));
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
