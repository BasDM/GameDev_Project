using Microsoft.Xna.Framework;

namespace GameDev_Project.Camera_s
{
    public class FollowingCamera
    {
        public Vector2 Position;

        public FollowingCamera(Vector2 position)
        {
            this.Position = position;
        }

        public void Follow(Vector2 targetPosition, Rectangle screenSize)
        {
            Position = new Vector2(
                targetPosition.X - (screenSize.Width / 2),
                targetPosition.Y - (screenSize.Height / 2)
            );
        }
    }
}