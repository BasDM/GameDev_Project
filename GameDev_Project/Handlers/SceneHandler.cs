using GameDev_Project.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameDev_Project.Handlers
{
    public class SceneHandler
    {
        public Dictionary<SceneType, Scene> sceneDictionary { get; set; }
        public Scene CurrentScene { get; set; }

        public SceneHandler()
        {
            sceneDictionary = new Dictionary<SceneType, Scene>();
        }

        public void SetScene(SceneType sceneType)
        {
            CurrentScene = sceneDictionary[sceneType];
            CurrentScene.LoadContent();
        }
        public void AddScene(SceneType sceneType, Scene scene)
        {
            sceneDictionary.Add(sceneType,scene);
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
