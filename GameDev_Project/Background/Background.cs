using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.AreaGameComponents
{
    public class Background
    {
        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 cameraPosition, Rectangle screen)
        {
            // Draw the background texture at the position offset by the camera
            spriteBatch.Draw(texture, new Rectangle((int)cameraPosition.X, (int)cameraPosition.Y, screen.Width, screen.Height), Color.White);
        }
    }
}
