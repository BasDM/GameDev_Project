using GameDev_Project.AnimationLogic;
using GameDev_Project.Events;
using GameDev_Project.Interfaces;
using GameDev_Project.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;

namespace GameDev_Project.Characters
{
    public class Enemy : Character, IGameObject
    {
        private Hero toFollow;
        private const float followRange = 300.0f;
        private Texture2D enemyTexture;

        private Animation currentAnimation;
        private Animation idleAnimation;
        private Animation walkingAnimation;

        private float direction;

        public Enemy(Texture2D _enemyTexture, GraphicsDevice graphicsDevice, Hero hero)
        {
            toFollow = hero;

            this.Width = 64;
            this.Height = 64;

            this.enemyTexture = _enemyTexture;
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
            this.BoundingBox = new Rectangle((int)this.Position.X + 15, (int)this.Position.Y + 16, Width - 35, Height -30);

            AddIdleAnimation();
            AddWalkingAnimation();
        }
        public void Move()
        {
            //===Follow logic===
            if (Math.Abs(this.Position.X - toFollow.Position.X) <= followRange)
            {
                direction = Math.Sign(toFollow.Position.X - this.Position.X); // -1 for left, +1 for right
                this.Speed = new Vector2(direction * MaxHorizontalSpeed, this.Speed.Y);
            }
            else
            {
                this.Speed = new Vector2(0, this.Speed.Y);
            }

            this.Speed = new Vector2(this.Speed.X, this.Speed.Y + Gravity);
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
            if (verticalCollidables.Any(o => futureVerticalBoundingBox.Intersects(o.BoundingBox) && this.Position.Y <= o.BoundingBox.Top))
                verticalMovement = 0;
            else
                verticalMovement += Gravity;

            // Update speed and position
            Speed = new Vector2(horizontalMovement, verticalMovement);
            Position += Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Debug)
            {
                spriteBatch.Draw(Texture, this.BoundingBox, Color.Red);
            }
            spriteBatch.Draw(enemyTexture, new Rectangle((int)this.Position.X, (int)this.Position.Y, Width, Height), currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0,new Vector2(0,0), horizontalFlip, 0f);
        }

        public override void Update(GameTime gameTime)
        {
            Move();
            if(direction == 0)
            {
                currentAnimation = idleAnimation;
            }
            else
            {
                currentAnimation = walkingAnimation;
                if (direction == -1)
                    horizontalFlip = SpriteEffects.FlipHorizontally;
                else
                    horizontalFlip = SpriteEffects.None;
            }

            if (BoundingBox.Intersects(GameScene.hero.BoundingBox))
            {
                GameScene.hero.GetHit(1);
            }

            currentAnimation.Update(gameTime);
            BoundingBox = new Rectangle((int)this.Position.X + 15, (int)this.Position.Y + 16, Width - 35, Height - 30);
        }

        public void AddIdleAnimation()
        {
            idleAnimation = new Animation();
            for (int i = 0; i < 4; i++)
            {
                idleAnimation.AddFrame(new AnimationFrame(new Rectangle(Width * i, 192, Width, Height)));
            }
        }

        public void AddWalkingAnimation()
        {
            walkingAnimation = new Animation();
            for (int i = 0; i < 12; i++)
            {
                walkingAnimation.AddFrame(new AnimationFrame(new Rectangle(Width * i, 128, Width, Height)));
            }
        }
    }
}
