﻿using GameDev_Project.Characters;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameDev_Project.Events
{
    public static class CollisionHandler
    {
        public static List<ICollidable> collidableList = new List<ICollidable>();
        public static List<Character> characters = new List<Character>();

        public static List<ICollidable>? CollidingWithObject(Rectangle boundingBoxToCheck)
        {
            return collidableList.FindAll(o => boundingBoxToCheck.Intersects(o.BoundingBox));
        }
        public static void AddCollidable(ICollidable collidable)
        {
            collidableList.Add(collidable);
        }

        public static void AddCharacter(Character character)
        {
            characters.Add(character);
        }

        public static void RemoveCollidable(ICollidable collidable)
        {
            int i = collidableList.IndexOf(collidable);
            if (i >= 0)
            {
                collidableList.Remove(collidable);
            }
        }

        public static void RemoveCharacter(Character character)
        {
            int i = characters.IndexOf(character);
            if (i >= 0)
            {
                characters.Remove(character);
            }
        }

        public static void FlushCollidables()
        {
            collidableList.Clear();
            characters.Clear();
        }
    }
}
