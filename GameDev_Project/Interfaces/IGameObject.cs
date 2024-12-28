using Microsoft.Xna.Framework;

namespace GameDev_Project.Interfaces
{
    public interface IGameObject:IAreaGameComponent
    {
        void Update(GameTime gameTime);
    }
}
