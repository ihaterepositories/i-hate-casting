using Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Interfaces;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.Moving.Base
{
    public class Mover
    {
        private IObstaclesBypassService _obstaclesBypassService;
        
        public void AssignObstaclesBypasser(IObstaclesBypassService obstaclesBypassService)
        {
            _obstaclesBypassService = obstaclesBypassService;
        }

        protected Vector2 CalculateBypassDirection()
        {
            if (_obstaclesBypassService == null)
                return Vector2.zero;
            
            return _obstaclesBypassService.CalculateBypassDirection();
        }
    }
}