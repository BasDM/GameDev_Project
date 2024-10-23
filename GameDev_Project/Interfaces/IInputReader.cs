using Microsoft.Xna.Framework;

namespace GameDev_Project.Interfaces
{
    public interface IInputReader
    {
        Vector2 ReadInput();
        public bool IsDestinationInput { get; }
    }
}
