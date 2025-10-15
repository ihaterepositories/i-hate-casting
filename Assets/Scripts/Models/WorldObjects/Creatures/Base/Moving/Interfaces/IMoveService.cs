using Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Interfaces;

namespace Models.WorldObjects.Creatures.Base.Moving.Interfaces
{
    public interface IMoveService
    { 
        public void Move();
        public void AssignObstaclesBypasser(IObstaclesBypassService obstaclesBypassService);
    }
}