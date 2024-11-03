using GameDev_Project.Characters;
using GameDev_Project.GameComponents;
using GameDev_Project.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using GameDev_Project.Factories;

namespace GameDev_Project
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D texture;
        Hero hero;

        Color backgroundColor = Color.CornflowerBlue;
        List<Block> blocks = new List<Block>();
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
            // TODO: Add your initialization logic here
            base.Initialize();
            CreateBlocks();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            texture = Content.Load<Texture2D>("Goblin Mech Rider Sprite Sheet");
            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            hero = new Hero(texture, new KeyboardReader(), GraphicsDevice);
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

            // TODO: Add your update logic here
            hero.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (var item in blocks)
            {
                item.Draw(_spriteBatch);
            }
            hero.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CreateBlocks()
        {
            for (int l = 0; l < gameBoard.GetLength(0); l++)
            {
                for (int k = 0; k < gameBoard.GetLength(1); k++)
                {
                    blocks.Add(BlockFactory.CreateBlockWithInt(gameBoard[l, k], k, l,GraphicsDevice));
                }
            }
        }
    }
}
