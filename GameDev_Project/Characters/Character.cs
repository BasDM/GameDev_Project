using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameDev_Project.Characters
{
    public class Character:IGameObject,ICollideWithEvent
    {
        private List<Character> observers;
        public IInputReader inputReader;

        public bool debug = true;
        public Rectangle BoundingBox { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; } = new Vector2(0, 0);

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
        public void registerObserver()
        {
            observers.Add(this);
        }

        public void removeObserver()
        {
            int i = observers.IndexOf(this);

        }
        public void notifyObservers()
        {
            throw new System.NotImplementedException();
        }
    }
}
