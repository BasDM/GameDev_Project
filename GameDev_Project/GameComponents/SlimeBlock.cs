﻿using GameDev_Project.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.GameComponents
{
    public class SlimeBlock : Block
    {
        public SlimeBlock(int x, int y, GraphicsDevice graphics) : base(x,y,graphics)
        {
            BoundingBox = new Rectangle(x, y, 100, 50);
            Passable = true;
            Color = Color.GreenYellow;
            Texture = new Texture2D(graphics, 1, 1);
            Texture.SetData(new[] { Color.White });
            CollideWithEvent = new SlowEvent();
        }
    }
}