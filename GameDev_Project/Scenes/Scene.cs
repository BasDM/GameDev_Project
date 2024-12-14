using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Scenes
{
    public class Scene
    {
        public Game1 game;

        public Scene(Game1 game)
        {
            this.game = game;
        }

        public virtual void LoadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }

    }
}
