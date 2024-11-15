using GameDev_Project.Characters;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameDev_Project.Events
{
    public class CollisionHandler
    {   
        private List<ICollidable> collidableList = new List<ICollidable>();
        private List<Character> characters = new List<Character>();
        
        public void Update(GameTime gameTime)
        {
            foreach(Character c in characters){
                foreach(ICollidable otherObject in collidableList)
                {
                    if (c.Intersects(otherObject))
                    {
                        NotifyObservers(c, otherObject);
                    }
                }
            }
            
        }
        public void AddCollidable(ICollidable collidable)
        {
            collidableList.Add(collidable);
        }

        public void AddCharacter(Character character)
        {
            characters.Add(character);
        }

        public void RemoveCollidable(ICollidable collidable)
        {

            int i = collidableList.IndexOf(collidable);
            if( i>=0)
            {
                collidableList.Remove(collidable);
            }
        }

        public void RemoveCharacter(Character character)
        {
            int i = characters.IndexOf(character);
            if (i >= 0)
            {
                characters.Remove(character);
            }
        }

        public void NotifyObservers(Character collidable, ICollidable collidingWith)
        {
            collidable.HandleCollision(collidingWith);
            collidingWith.HandleCollision(collidable);
        }
    }
}
