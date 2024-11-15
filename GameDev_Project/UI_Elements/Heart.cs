using GameDev_Project.Characters;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
namespace GameDev_Project.UI
{
    public class Heart : IUserInterfaceComponent
    {
        public Texture2D Texture { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Vector2 Position { get; set; } = new Vector2(0, 0);
        public Rectangle BoundingBox { get; set; }

        public Heart(Texture2D texture, int width, int height, Vector2  position, Rectangle boundingBox)
        {
            this.Texture = texture;
            this.width = width;
            this.height = height;
            this.Position = position;
            this.BoundingBox = boundingBox;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, BoundingBox, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
