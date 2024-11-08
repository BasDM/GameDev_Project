using Microsoft.Xna.Framework;
using System;

namespace GameDev_Project.Interfaces
{
    public interface ICollidable
    {
        Rectangle BoundingBox { get; set; }
        void HandleCollision(ICollidable other);
    }
}
