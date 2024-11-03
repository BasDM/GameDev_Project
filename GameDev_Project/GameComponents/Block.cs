using GameDev_Project.Characters;
using GameDev_Project.Events;
using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev_Project.GameComponents
{
    public class Block : IAreaGameComponent
    {
        public Rectangle BoundingBox { get; set; }
        public bool Passable { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        public ICollideWithEvent CollideWithEvent { get; set; }
        public Vector2 Position { get; set; }

        public Block(int x, int y, GraphicsDevice graphics)
        {
            BoundingBox = new Rectangle(x, y, 100, 50);
            Passable = false;
            Color = Color.Green;
            Texture = new Texture2D(graphics, 1, 1);
            Texture.SetData(new[] { Color.White });
            CollideWithEvent = new NoEvent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, BoundingBox, Color);
        }

        public bool Intersects(IGameObject other)
        {
            return BoundingBox.Intersects(other.BoundingBox);
        }

        public virtual void IsCollideWithEvent(Character collider)
        {
            CollideWithEvent.Execute();
        }

    }
}
