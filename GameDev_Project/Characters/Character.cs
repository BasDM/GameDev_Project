using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameDev_Project.Characters
{
    public class Character:IGameObject,ICollidable
    {
        public IInputReader inputReader;

        public bool debug = false;
        public Rectangle BoundingBox { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; } = new Vector2(0, 0);
        public bool IsColliding { get; set; }
        public ICollidable CollidingWith { get;set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, BoundingBox, Color.White);
        }

        public bool Intersects(IGameObject other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }

        public void Update(GameTime gametime)
        {

        }
    }
}
