using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameDev_Project.UI
{
    public class Button : UIElement
    {
        public String Text { get; set; }
        public SpriteFont Font { get; set; }
        public Button(Texture2D texture, int width, int height, Vector2 position, string text, SpriteFont font, Action onClick) : base(texture, width, height, position)
        {
            Text = text;
            Font = font;
            OnClick = onClick;
            Rectangle = new Rectangle((int)position.X, (int)position.Y, 400, 50);
        }
        public Rectangle Rectangle { get; set; }
        public Action OnClick { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
            spriteBatch.DrawString(Font, Text, new Vector2(Position.X * 2 - 10,Position.Y + 20), Color.White);
        }

        public void Update(MouseState mouseState)
        {
            if (Rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                OnClick.Invoke();
            }
        }
    }
}
