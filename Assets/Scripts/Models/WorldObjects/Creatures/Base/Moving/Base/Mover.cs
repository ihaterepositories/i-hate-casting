using Models.WorldObjects.Creatures.Base.ObstaclesBypassing.Interfaces;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.Moving.Base
{
    public class Mover
    {
        private IObstaclesBypassService _obstaclesBypassService;
        
        protected readonly CreatureStatsCalculator _statsCalculator;
        protected readonly Rigidbody2D _rb;
        
        protected Mover(
            CreatureStatsCalculator statsCalculator,
            Rigidbody2D rb )
        {
            _statsCalculator = statsCalculator;
            _rb = rb;
        }
        
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