using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameDev_Project.Characters
{
    public abstract class Character : IGameObject, ICollidable
    {
        public IInputReader inputReader;
        public bool debug = false;
        public int hp;

        public Rectangle BoundingBox { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; } = new Vector2(0, 0);
        //collision
        public bool IsColliding { get; set; }
        public ICollidable CollidingWith { get; set; }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, BoundingBox, Color.White);
        }

        public bool Intersects(ICollidable other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }

        public abstract void Update(GameTime gameTime);

        public abstract void HandleCollision(ICollidable other);

        
    }
}
