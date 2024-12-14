using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.Characters.Enemies
{
    public class EnemyHealthBar
    {
        private Character enemy;
        private int heartWidth;
        private Vector2 heartPosition;
        private Texture2D heartTexture;

        public EnemyHealthBar(Character _enemy, ContentManager _content, int _heartWidth, int _heartHeight)
        {
            enemy = _enemy;
            heartTexture = _content.Load<Texture2D>("heart_16x16");
            heartWidth = _heartWidth;
            heartPosition = new Vector2(enemy.Position.X, enemy.BoundingBox.Top - 20);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < enemy.Health; i++)
            {
                spriteBatch.Draw(heartTexture, new Vector2(heartPosition.X + i * heartWidth, heartPosition.Y), Color.White);
            }
        }
        public void Update(GameTime gameTime)
        {
            heartPosition = new Vector2(enemy.Position.X , enemy.BoundingBox.Top - 20);
        }
    }
}
