using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Characters
{
    public class Enemy : Character, IGameObject
    {

        public Enemy(GraphicsDevice graphicsDevice)
        {
            this.width = 30;
            this.height = 30;

            Texture = new Texture2D(graphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.White });
            this.Position = new Vector2(10, 20);
            this.health = 2;
            this.maxHealth = health;
            this.dead = false;
            this.gravity = 1f;
            this.BoundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, width, height);
        }
        public void Move(Hero hero)
        {
            
            if (this.Position.X >= hero.Position.X && this.Position.X <= hero.Position.X)
            {
                this.Position = new Vector2(this.Position.X + 1, this.Position.Y);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, this.BoundingBox, Color.Red);
        }

        public override void Update(GameTime gameTime)
        {
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, width, height);
        }
    }
}
