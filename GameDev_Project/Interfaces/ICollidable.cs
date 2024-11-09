using Microsoft.Xna.Framework;
using System;

namespace GameDev_Project.Interfaces
{
    public interface ICollidable
    {
        int width { get; set; }
        int height{ get; set; }
        Vector2 Position { get; set; }
        Rectangle BoundingBox { get; set; }
        void HandleCollision(ICollidable other);
    }
}
