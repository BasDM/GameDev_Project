using GameDev_Project.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.Scenes.VictoryScreen
{
    public class VictoryScene : Scene
    {
        public Texture2D BackgroundTexture;
        public Rectangle Screen;
        public List<Button> buttons;
        public SpriteFont buttonFont;
        private Song themeSong;
        private float soundEffectVolume = 0.40f;
        public VictoryScene(Game1 game) : base(game)
        {

        }
        public override void LoadContent()
        {
            BackgroundTexture = game.Content.Load<Texture2D>("VictoryBackground");
            themeSong = game.Content.Load<Song>(@"music\VictoryTheme");
            Screen = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            buttons = new List<Button>();
            Texture2D playButtonTexture = game.Content.Load<Texture2D>("button");
            buttonFont = game.Content.Load<SpriteFont>(@"Fonts\CustomFont");
            buttons.Add(new Button(playButtonTexture, 0, 0, new Vector2(Screen.Width / 4, Screen.Height / 2), "Menu", buttonFont, () =>
            {
                game.SceneHandler.SetScene(SceneType.startMenuScene);
            }));

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = soundEffectVolume;
            MediaPlayer.Play(themeSong);

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            foreach (var button in buttons)
            {
                button.Update(mouseState);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.M) && MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.M) && MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = soundEffectVolume;
                MediaPlayer.Play(themeSong);
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
