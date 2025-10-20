using Models.WorldObjects.Creatures.Base.ObstaclesBypassing.Interfaces;

namespace Models.WorldObjects.Creatures.Base.Moving.Interfaces
{
    public interface IMoveService
    { 
        /// <summary>
        /// Invoke this method in the Update
        /// to enable moving.
        /// </summary>
        public void EnableMove();
        public void AssignObstaclesBypasser(IObstaclesBypassService obstaclesBypassService);
    }
}