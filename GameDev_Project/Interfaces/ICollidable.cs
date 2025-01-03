﻿using Microsoft.Xna.Framework;

namespace GameDev_Project.Interfaces
{
    public interface ICollidable
    {
        int Width { get; set; }
        int Height{ get; set; }
        Vector2 Position { get; set; }
        Rectangle BoundingBox { get; set; }
    }
}
