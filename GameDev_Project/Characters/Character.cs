using GameDev_Project.AnimationLogic;
using GameDev_Project.Events;
using GameDev_Project.GameComponents;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameDev_Project.Characters
{
    public abstract class Character : IGameObject, ICollidable
    {
        public IInputReader InputReader;
        public bool Debug = false;

        public Rectangle BoundingBox { get; set; }
        public Texture2D boundingBoxTexture { get; set; }
        public SpriteEffects horizontalFlip = SpriteEffects.None;
        public Vector2 Position { get; set; } = new Vector2(0, 0);
        public Animation currentAnimation { get; set; }

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
                return; // Character is immune, do not apply damage
            }

            Health -= damage;
            ImmunityTimer = 2f; // Start immunity timer

            if (Health <= 0)
            {
                Dead = true;
            }
        }

        public void Attack(Character other)
        {
            if (BoundingBox.Intersects(other.BoundingBox) && other.ImmunityTimer <= 0)
            {
                other.GetHit(1);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Debug)
            {
                spriteBatch.Draw(boundingBoxTexture, BoundingBox, Color.Red);
            }
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
        
        public virtual void Move()
        {
            Speed = new Vector2(Speed.X, Speed.Y + Gravity);
            Speed = new Vector2(Math.Clamp(Speed.X, -4, MaxHorizontalSpeed), Math.Clamp(Speed.Y, -30, MaxVerticalSpeed));

            float horizontalMovement = Speed.X;
            Rectangle futureHorizontalBoundingBox = new Rectangle(
                (int)(BoundingBox.X + horizontalMovement),
                BoundingBox.Y,
                BoundingBox.Width,
                BoundingBox.Height
            );

            List<ICollidable> horizontalCollidables = CollisionHandler.CollidingWithObject(futureHorizontalBoundingBox);
            if (horizontalCollidables.Any(o => futureHorizontalBoundingBox.Intersects(o.BoundingBox)))
            {
                horizontalMovement = 0;
            }

            float verticalMovement = Speed.Y;
            Rectangle futureVerticalBoundingBox = new Rectangle(
                BoundingBox.X,
                (int)(BoundingBox.Y + verticalMovement),
                BoundingBox.Width,
                BoundingBox.Height
            );

            List<ICollidable> verticalCollidables = CollisionHandler.CollidingWithObject(futureVerticalBoundingBox);
            if (verticalCollidables.Any(o => futureVerticalBoundingBox.Intersects(o.BoundingBox) && Position.Y <= o.BoundingBox.Top))
            {
                CheckIfInVoid(verticalCollidables);
                verticalMovement = 0;
            }
            else
                verticalMovement += Gravity;

            // Update speed and position
            Speed = new Vector2(horizontalMovement, verticalMovement);
            Position += Speed;
        }
        public void CheckIfInVoid(List<ICollidable> verticalCollidables)
        {
            if (verticalCollidables.Any(o => o is VoidBlock))
            {
                Health = 0;
            }
        }
    }
}
