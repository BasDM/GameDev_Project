using GameDev_Project.Characters;
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
        private List<Character> characters;
        
        public void Update(GameTime gameTime)
        {
            //Detect for collisions
            //TODO
            
        }
        public void AddCollidable(ICollidable collidable)
        {
            collidableList.Add(collidable);
        }

        public void AddCharacter(Character character)
        {
            characters.Add(character);
        }

        public void removeCollidable(ICollidable collidable)
        {

            int i = collidableList.IndexOf(collidable);
            if( i>=0)
            {
                collidableList.Remove(collidable);
            }
        }

        public void removeCharacter(Character character)
        {
            int i = characters.IndexOf(character);
            if (i >= 0)
            {
                characters.Remove(character);
            }
        }

        public void notifyObservers(Character collidable, ICollidable collidingWith)
        {
            collidable.HandleCollision(collidingWith);
            collidingWith.HandleCollision(collidable);
        }
    }
}
