using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.Managers
{
    public class TextureManager
    {
        private Game1 _game;
        public Texture2D _heroTexture;
        public Texture2D _blockTexture;
        public Texture2D _backgroundTexture;
        public Texture2D _enemyTexture;
        public Texture2D _runawayEnemyTexture;
        public Texture2D _flyingEnemyTexture;
        public TextureManager(Game1 game)
        {
            _game = game;
            SetTextures();
        }
        public void SetTextures()
        {
           _heroTexture = _game.Content.Load<Texture2D>("NightBorne");
           _blockTexture = _game.Content.Load<Texture2D>("[64x64] Dungeon Bricks Plain");
           _backgroundTexture = _game.Content.Load<Texture2D>("crystal_cave_background");
           _enemyTexture = _game.Content.Load<Texture2D>("Skeleton enemy");
           _runawayEnemyTexture = _game.Content.Load<Texture2D>("Rotbo-Sheet");
           _flyingEnemyTexture = _game.Content.Load<Texture2D>("DroneR-Sheet");
        }
    }
}
