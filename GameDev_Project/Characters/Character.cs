using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameDev_Project.Characters
{
    public abstract class Character : IGameObject, ICollidable
    {
        public IInputReader InputReader;
        public bool Debug = true;

        public Rectangle BoundingBox { get; set; }
        public Texture2D Texture { get; set; }
        public SpriteEffects horizontalFlip = SpriteEffects.None;
        public Vector2 Position { get; set; } = new Vector2(0, 0);

        //collision
        public ICollidable CollidingWith { get; set; }

        //physics
        public float Gravity { get; set; }
        public bool IsOnGround { get; set; }

        //speed (varies from character to character so yes to setters)
        public Vector2 Speed { get; set; }
        public float MaxVerticalSpeed { get; set; }
        public float MaxHorizontalSpeed { get; set; }
        public Vector2 Acceleration { get; set; }

        //should always return 0.5f. Never changed so no set
        public float AccelerationMultiplier { get { return 0.5f; } }

        public int Width { get; set; }
        public int Height { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public bool Dead { get; set; }

        public float ImmunityTimer { get; set; } = 0f;

        public Character()
        {
            //make health 1 and maxHealth refer to health so I only need to change 1 value
            Health = 1;
            MaxHealth = Health;
            Dead = false;
            Gravity = 1f;
        }

        public void GetHit(int damage)
        {
            if (ImmunityTimer > 0)
            {
                return; // Hero is immune, do not apply damage
            }

            Health -= damage;
            ImmunityTimer = 2f; // Start immunity timer

            if (Health <= 0)
            {
                Dead = true;
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
