﻿using GameDev_Project.Handlers;
using GameDev_Project.Scenes;
using GameDev_Project.Scenes.BeginScreen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameDev_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public SceneHandler SceneHandler { get; set; }

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
            SceneHandler = new SceneHandler();
            SceneHandler.AddScene(Scenes.SceneType.gameScene, new GameScene(this));
            SceneHandler.AddScene(Scenes.SceneType.startMenuScene, new StartMenuScene(this));
            SceneHandler.AddScene(Scenes.SceneType.level2, new Level2(this));
            SceneHandler.SetScene(SceneType.startMenuScene);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            SceneHandler.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SceneHandler.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}
