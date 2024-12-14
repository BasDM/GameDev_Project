using GameDev_Project.AreaGameComponents;
using GameDev_Project.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.Scenes.BeginScreen
{
    public class StartMenuScene : Scene
    {
        private Texture2D BackgroundTexture;
        private Rectangle Screen;
        private List<Button> buttons;
        private SpriteFont buttonFont;
        public StartMenuScene(Game1 game) : base(game)
        {
            LoadContent();
        }

        public override void LoadContent()
        {
            BackgroundTexture = game.Content.Load<Texture2D>("StartScreenBackground");
            Screen = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            buttons = new List<Button>();
            Texture2D playButtonTexture = game.Content.Load<Texture2D>("button");
            buttonFont = game.Content.Load<SpriteFont>(@"Fonts\CustomFont");
            buttons.Add(new Button(playButtonTexture,0,0,new Vector2(Screen.Width/4, Screen.Height/2),"Play",buttonFont, () =>
            {
                game.SceneHandler.SetScene(SceneType.gameScene);
            }));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            foreach (var button in buttons)
            {
                button.Update(mouseState);
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(BackgroundTexture, Screen, Color.White);
            foreach (var button in buttons)
            {
                button.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(spriteBatch);
        }
    }
}
