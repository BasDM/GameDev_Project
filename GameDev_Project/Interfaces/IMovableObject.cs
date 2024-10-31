using Microsoft.Xna.Framework;

namespace GameDev_Project.Interfaces
{
    public interface IMovableObject : ICollide
    {
        void Update(GameTime gameTime);


    }
}
