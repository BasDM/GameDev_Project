using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.Events
{
    public class CollisionHandler
    {
        private List<ICollidable> collidableList;
        
        public void HandleCollision(ICollidable currentEvent)
        {
            int damage;
            int paceMultiplier = 0;
            switch (currentEvent)
            {
                case NoEvent:
                    break;
                case DamageEvent:
                    new DamageEvent(10);
                    break;
                case SlowEvent:
                    paceMultiplier = -1;
                    break;
            }
        }
        public void Update(GameTime gameTime)
        {
            
        }
        public void registerObserver(ICollidable collidable)
        {
            collidableList.Add(collidable);
        }
        public void removeObserver(ICollidable collidable)
        {
            int i = collidableList.IndexOf(collidable);
            if( i>=0)
            {
                collidableList.Remove(collidable);
            }
        }
        public void notifyObservers(ICollidable collidable, ICollidable collidingWith)
        {
            collidable.CollidingWith = collidingWith;
        }
    }
}
