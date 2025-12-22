using Models.Creatures.Services.ObstaclesBypassing.Interfaces;

namespace Models.Creatures.Services.Moving.Interfaces
{
    public interface ICreatureMover
    {
        public bool IsMoving { get; }
        public bool IsDirectedToTheRight { get; }
        
        /// <summary>
        /// Invoke this method in the FixedUpdate
        /// to enable moving.
        /// </summary>
        public void EnableMove();
        public void AssignObstaclesBypasser(IObstaclesBypassForCreatureService obstaclesBypassForCreatureService);
    }
}