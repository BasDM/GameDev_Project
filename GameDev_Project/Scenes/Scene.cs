using GameDev_Project.Characters;
using GameDev_Project.Characters.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Scenes
{
    public class Scene
    {
        public Game1 game;
        public static Hero Hero;
        public static Enemy Enemy;
        public static FlyingEnemy FlyingEnemy;
        public static RunawayEnemy RunawayEnemy;

        public Scene(Game1 game)
        {
            this.game = game;
        }

        public virtual void LoadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }

    }
}
