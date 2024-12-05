﻿using GameDev_Project.Characters;
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

        Texture2D texture;
        Hero hero;
        UserInterface ui;

        //Tiles
        List<Block> blocks = new List<Block>();
        public static Texture2D blockTexture;
        public static Texture2D slimeBlockTexture;
        public static Texture2D trapBlockTexture;

        //Background
        public static Rectangle screen;
        public static Texture2D backgroundTexture;
        public Background background;

        //Sounds
        private SoundEffect slashEffect;
        private float soundEffectVolume = 0.40f;
        private SoundEffectInstance slashEffectInstance;

        //Camera
        private FollowingCamera camera;

        //Music
        private Song themeSong;


        int[,] gameBoard = new int[,]
        {
  {0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {0,1,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,1},
  {0,0,0,0,1,0,0,0,1,1,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,1,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,1},
  {0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,0,1,0,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,1},
  {0,1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0},
  {0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,1},
  {0,0,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,1,1},
  {1,0,0,0,0,1,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,1,0,1,0,1,0,0,0,0,1,0,0,0,1,1,1},
  {0,1,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,1,1,1,1,1,1}
};

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
            CreateBlocks();
            camera = new FollowingCamera(Vector2.Zero);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Content.Load<Texture2D>("NightBorne");
            blockTexture = Content.Load<Texture2D>("[64x64] Dungeon Bricks Plain");

            backgroundTexture = Content.Load<Texture2D>("crystal_cave_background");

            slashEffect = Content.Load<SoundEffect>(@"sounds\sword-slash-and-swing-185432");
            slashEffectInstance = slashEffect.CreateInstance();
            themeSong = Content.Load<Song>(@"music\dark8bitThemesong");
            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            hero = new Hero(texture, new KeyboardReader(), GraphicsDevice);
            CollisionHandler.AddCharacter(hero);
            ui = new UserInterface(hero, Content, 20, 20, new Vector2(10, 10));
            background = new Background();
            screen = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = soundEffectVolume;
            MediaPlayer.Play(themeSong);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                hero.ChangeInput(new KeyboardReader());
            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                hero.ChangeInput(new MouseReader());
            }

            //Attack sound effect
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (slashEffectInstance.State != SoundState.Playing)
                {
                    slashEffectInstance.Volume = soundEffectVolume;
                    slashEffectInstance.Play();
                }
            }

            //Play or pause music
            if (Keyboard.GetState().IsKeyDown(Keys.M) && MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.M) && MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = soundEffectVolume;
                MediaPlayer.Play(themeSong);
            }

            hero.Update(gameTime);
            camera.Follow(hero.Position, new Rectangle(0, 0, screen.Width, screen.Height));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Matrix cameraTransform = Matrix.CreateTranslation(new Vector3(-camera.Position, 0));

            _spriteBatch.Begin(transformMatrix: cameraTransform, samplerState: SamplerState.PointClamp);
            background.Draw(_spriteBatch, backgroundTexture, camera.Position, screen);
            foreach (var item in blocks)
            {
                item.Draw(_spriteBatch);
            }
            hero.Draw(_spriteBatch);
            _spriteBatch.End();
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            ui.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CreateBlocks()
        {
            for (int l = 0; l < gameBoard.GetLength(0); l++)
            {
                for (int k = 0; k < gameBoard.GetLength(1); k++)
                {
                    int val = gameBoard[l, k];
                    if (val != 0)
                    {
                        blocks.Add(BlockFactory.CreateBlockWithInt(val, k, l));
                    }
                }
            }

            foreach (var block in blocks)
            {
                CollisionHandler.AddCollidable(block);
            }
        }
    }
}
