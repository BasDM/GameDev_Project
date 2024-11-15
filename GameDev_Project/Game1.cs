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

namespace GameDev_Project
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D texture;
        Hero hero;
        UserInterface ui;

        Color backgroundColor = Color.CornflowerBlue;
        List<Block> blocks = new List<Block>();
        Texture2D blockTexture;
        Texture2D slimeBlockTexture;
        Texture2D trapBlockTexture;
        CollisionHandler collisionHandler;

        //Sounds
        private SoundEffect slashEffect;
        private float soundEffectVolume = 0.25f;
        private SoundEffectInstance slashEffectInstance;

        int[,] gameBoard = new int[,]
        {
            { 1,1,1,1,1,1,1,1 },
            { 0,0,1,1,0,1,1,1 },
            { 1,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,0,1 },
            { 1,0,0,0,0,0,0,2 },
            { 1,0,1,1,1,1,1,2 },
            { 1,0,0,0,0,0,0,0 },
            { 1,1,1,1,1,1,1,1 }
        };

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            CreateBlocks();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Content.Load<Texture2D>("NightBorne");
            blockTexture = Content.Load<Texture2D>("Dungeon Tile Set");
            //slimeBlockTexture = Content.Load<Texture2D>(" ");
            //trapBlockTexture = Content.Load<Texture2D>(" ");
            slashEffect = Content.Load<SoundEffect>(@"sounds\sword-slash-and-swing-185432");

            slashEffectInstance = slashEffect.CreateInstance();
            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            hero = new Hero(texture, new KeyboardReader(), GraphicsDevice);
            collisionHandler = new CollisionHandler();
            collisionHandler.AddCharacter(hero);
            //ui = new UserInterface(hero.maxHealth,heartTexture,heartWidth, heartHeight, heartBoundingBox);
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
                if(slashEffectInstance.State != SoundState.Playing)
                {
                    slashEffectInstance.Volume = soundEffectVolume;
                    slashEffectInstance.Play();
                }
            }

            hero.Update(gameTime);
            collisionHandler.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            _spriteBatch.Begin();
            foreach (var item in blocks)
            {
                item.Draw(_spriteBatch);
            }
            hero.Draw(_spriteBatch);
            //ui.Draw(_spriteBatch);
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
                        blocks.Add(BlockFactory.CreateBlockWithInt(val, k, l, GraphicsDevice));
                    }
                }
            }

            foreach (var block in blocks)
            {
                collisionHandler.AddCollidable(block);
            }
        }
    }
}
