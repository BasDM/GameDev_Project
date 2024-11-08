using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameDev_Project.Interfaces
{
    public interface IGameObject:IAreaGameComponent
    {
        void Update(GameTime gameTime);
    }
}
