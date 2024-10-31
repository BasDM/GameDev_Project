using GameDev_Project.Characters;
using GameDev_Project.GameComponents;
using GameDev_Project.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDev_Project
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D texture;
        Hero hero;

        Block whiteBox;
        Color backgroundColor = Color.CornflowerBlue;
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
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            texture = Content.Load<Texture2D>("Goblin Mech Rider Sprite Sheet");
            whiteBox = new Block(GraphicsDevice,new Vector2(400,240));
            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            hero = new Hero(texture, new KeyboardReader(),GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (hero.Intersects(whiteBox))
            {
                backgroundColor = Color.Black;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                hero.ChangeInput(new KeyboardReader());
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                hero.ChangeInput(new MouseReader());

            // TODO: Add your update logic here
            hero.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            whiteBox.Draw(_spriteBatch);
            hero.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
