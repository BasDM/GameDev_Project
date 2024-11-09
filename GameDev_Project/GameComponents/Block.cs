using GameDev_Project.Characters;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Threading;

namespace GameDev_Project.GameComponents
{
    public class Block : IAreaGameComponent
    {
        public int height { get; set; }
        public int width { get; set; }
        public Rectangle BoundingBox { get; set; }
        public bool Passable { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }


        public Block(int x, int y, GraphicsDevice graphics)
        {
            height = 80;
            width = 100;
            BoundingBox = new Rectangle(x, y, width, height);
            Passable = false;
            Color = Color.Green;
            Texture = new Texture2D(graphics, 1, 1);
            Texture.SetData(new[] { Color.White });
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, BoundingBox, Color);
        }

        public bool Intersects(ICollidable other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }

        
        public void HandleCollision(ICollidable collidable)
        {
            //Different per block, implement in specific type
            Debug.WriteLine("STOP TOUCHING MEEEE!!!");

            //TODO change acceleration of character
        }
    }
}
