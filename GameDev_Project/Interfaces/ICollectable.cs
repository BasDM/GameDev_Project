using GameDev_Project.Characters;

namespace GameDev_Project.Interfaces
{
    public interface ICollectable : IAreaGameComponent
    {
        void Collect(Hero hero);
    }
}
