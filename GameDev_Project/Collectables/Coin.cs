using GameDev_Project.Characters;
using GameDev_Project.Interfaces;
using GameDev_Project.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev_Project.Collectables
{
    public class Coin : ICollectable
    {
        public Texture2D boundingBoxTexture { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle BoundingBox { get; set; }

        private Texture2D coinTexture;
        private TextureManager textureManager;

        public Coin(Vector2 position, Game1 game)
        {
            TextureManager textureManager = new TextureManager(game);
            coinTexture = textureManager.coinTexture;
            Position = position;
        }

        public void Collect(Hero hero)
        {
            hero.CollectCoin();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(coinTexture, BoundingBox, Color.White);
        }
    }
}
