using GameDev_Project.Interfaces;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.IO;

namespace GameDev_Project.Input
{
    public class KeyboardReader : IInputReader
    {
        public bool IsDestinationInput => true;

        public Vector2 ReadInput()
        {
            KeyboardState state = Keyboard.GetState();
            var direction = Vector2.Zero;

            if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q))
            {
                direction.X -= 1;
                
            }
            if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
            {
                direction.X += 1;

            }
            if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
            {
                direction.Y += 1;

            }
            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Z))
            {
                direction.Y -= 1;

            }

            return direction;
        }
    }
}
