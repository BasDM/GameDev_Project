using GameDev_Project.Characters;
using GameDev_Project.GameComponents;
using GameDev_Project.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using GameDev_Project.Factories;
using GameDev_Project.Events;
using Microsoft.Xna.Framework.Content;
using GameDev_Project.UI;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using GameDev_Project.AreaGameComponents;
using GameDev_Project.Camera_s;
using System;

namespace GameDev_Project
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = false;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);
        }

        protected override void Initialize()
        {
            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            

            base.Draw(gameTime);
        }

        
    }
}
