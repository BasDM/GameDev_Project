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

        public Rectangle BoundingBox { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; } = new Vector2(0, 0);

        //collision
        public ICollidable CollidingWith { get; set; }

        //physics
        public float gravity { get; set; }

        //speed
        public Vector2 speed { get; set; }
        public float MaxVerticalSpeed { get; set; }
        public float MaxHorizontalSpeed { get; set; }
        public Vector2 _acceleration { get; set; }
        public float AccelerationMultiplier { get; set; } = 0.5f;

        public int width { get; set; }
        public int height { get; set; }
        public int health { get; set; }
        public int maxHealth { get; set; }
        public bool dead { get; set; }

        public Character()
        {
            //make health 1 and maxHealth refer to health so I only need to change 1 value
            health = 1;
            maxHealth = health;
            dead = false;
            gravity = 1f;
        }

        //TODO: Use GetHit when player gets hit by enemy or trap ...
        public void GetHit(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                dead = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, BoundingBox, Color.White);
        }

        public bool Intersects(ICollidable other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }

        public Vector2 Limit(Vector2 v, float min, float max)
        {
            float length = v.Length();

            if (length > max)
            {
                float ratio = max / length;
                v.X *= ratio;
                v.Y *= ratio;
            }
            else if (length < min && length > 0)
            {
                float ratio = min / length;
                v.X *= ratio;
                v.Y *= ratio;
            }
            return v;
        }

        public abstract void Update(GameTime gameTime);
    }
}
