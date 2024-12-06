using GameDev_Project.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameDev_Project.Handlers
{
    public class SceneHandler
    {
        //Dictionary so I can storage a string of the scene with the scene
        public Dictionary<string, Scene> sceneDictionary { get; set; }
        public Scene CurrentScene { get; set; }
        public void SetScene()
        {
            //TODO: Change the CurrentScene to a given scene;
            throw new NotImplementedException();
        }
        public void AddScene()
        {
            //Add given scene to dictionary here
            throw new NotImplementedException();
        }
        public void RemoveScene()
        {
            //To delete scenes from the dictionary.
            throw new NotImplementedException();
        }

        //=== Call the logic of the scene ===
        public void Update(GameTime gameTime)
        {
            //Call the update function of the currentScene (only if it's not null)
            if (CurrentScene != null)
            {
                CurrentScene.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Also only call draw on currentScene when it's not null
            if (CurrentScene != null)
            {
                CurrentScene.Draw(spriteBatch);
            }
        }
    }
}
