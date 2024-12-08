using GameDev_Project.Events;
using GameDev_Project.Interfaces;
using GameDev_Project.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameDev_Project.Characters
{
    public class Enemy : Character, IGameObject
    {
        private Hero toFollow;

        public Enemy(GraphicsDevice graphicsDevice, Hero hero)
        {
            toFollow = hero;

            this.Width = 30;
            this.Height = 30;

            Texture = new Texture2D(graphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.White });
            this.Position = new Vector2(400, 20);
            
            this.Speed = new Vector2(0, 0);
            this.Acceleration = new Vector2(0.9f, 0.9f);
            this.MaxHorizontalSpeed = 2;
            this.MaxVerticalSpeed = 80;

            this.Health = 2;
            this.MaxHealth = Health;
            this.Dead = false;
            this.Gravity = 1f;
            this.BoundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, Width, Height);
        }
        public void Move()
        {
            //===Follow logic===
            if (this.Position.X >= toFollow.Position.X && this.Position.X <= toFollow.Position.X)
            {
                this.Position = new Vector2(this.Position.X + 1, this.Position.Y);
            }

            this.Speed = new Vector2(toFollow.Speed.X, Speed.Y + Gravity);
            Speed = new Vector2(Math.Clamp(Speed.X, -4, MaxHorizontalSpeed), Math.Clamp(Speed.Y, -30, MaxVerticalSpeed));

            float horizontalMovement = Speed.X;
            Rectangle futureHorizontalBoundingBox = new Rectangle(
                (int)(BoundingBox.X + horizontalMovement),
                BoundingBox.Y,
                BoundingBox.Width,
                BoundingBox.Height
                );

            List<ICollidable> horizontalCollidables = CollisionHandler.CollidingWithObject(futureHorizontalBoundingBox);
            if (horizontalCollidables.Any(o => BoundingBox.Left <= o.BoundingBox.Right || BoundingBox.Right >= o.BoundingBox.Left))
            {
                horizontalMovement = 0;
                Speed = new Vector2(horizontalMovement, Speed.Y);
            }

            float verticalMovement = Speed.Y;
            Rectangle futureVerticalBoundingBox = new Rectangle(
                BoundingBox.X,
                (int)(BoundingBox.Y + verticalMovement),
                BoundingBox.Width,
                BoundingBox.Height
                );

            List<ICollidable> verticalCollidables = CollisionHandler.CollidingWithObject(futureVerticalBoundingBox);
            if (verticalCollidables.Any(o => BoundingBox.Bottom <= o.BoundingBox.Top))
            {
                verticalMovement = 0;
            }
            else if (verticalCollidables.Any(o => BoundingBox.Top >= o.BoundingBox.Bottom))
            {
                verticalMovement = Gravity;
            }

            Speed = new Vector2(Speed.X, verticalMovement);
            Position += Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, this.BoundingBox, Color.Red);
        }

        public override void Update(GameTime gameTime)
        {
            Move();
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }
    }
}
