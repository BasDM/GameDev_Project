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
        private List<UIElement> buttons;
        public StartMenuScene(Game1 game) : base(game)
        {
            BackgroundTexture = game.Content.Load<Texture2D>("StartScreenBackground");
            Screen = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(BackgroundTexture, Screen, Color.White);
            spriteBatch.End();

            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
