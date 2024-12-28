using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Managers
{
    public class TextureManager
    {
        private Game1 _game;
        public Texture2D heroTexture;
        public Texture2D blockTexture;
        public Texture2D levelOneBackgroundTexture;
        public Texture2D levelTwoBackgroundTexture;
        public Texture2D enemyTexture;
        public Texture2D runawayEnemyTexture;
        public Texture2D flyingEnemyTexture;
        public TextureManager(Game1 game)
        {
            _game = game;
            SetTextures();
        }
        public void SetTextures()
        {
           heroTexture = _game.Content.Load<Texture2D>("NightBorne");
           blockTexture = _game.Content.Load<Texture2D>("[64x64] Dungeon Bricks Plain");
           levelOneBackgroundTexture = _game.Content.Load<Texture2D>("crystal_cave_background");
           levelTwoBackgroundTexture = _game.Content.Load<Texture2D>("level2Background");
           enemyTexture = _game.Content.Load<Texture2D>("Skeleton enemy");
           runawayEnemyTexture = _game.Content.Load<Texture2D>("Rotbo-Sheet");
           flyingEnemyTexture = _game.Content.Load<Texture2D>("DroneR-Sheet");
        }
    }
}
