namespace GameDev_Project.Interfaces
{
    public interface ICollideWithEvent
    {
        //ICollideWithEvent is a subject
        public void registerObserver();
        public void removeObserver();
        public void notifyObservers();
    }
}
