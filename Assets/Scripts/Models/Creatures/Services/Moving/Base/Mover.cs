using Models.Creatures.Services.ObstaclesBypassing.Interfaces;
using Models.Creatures.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.Moving.Base
{
    public class Mover
    {
        private IObstaclesBypassForCreatureService _obstaclesBypassForCreatureService;
        
        protected readonly ICreatureStatsCalculator _statsCalculateService;
        protected readonly Rigidbody2D _rb;
        
        public bool IsMoving => _rb.velocity.magnitude > 0.01f;
        public bool IsDirectedToTheRight => _rb.velocity.x > 0;
        
        protected Mover(
            ICreatureStatsCalculator statsCalculateService,
            Rigidbody2D rb)
        {
            _statsCalculateService = statsCalculateService;
            _rb = rb;
        }
        
        public void AssignObstaclesBypasser(IObstaclesBypassForCreatureService obstaclesBypassForCreatureService)
        {
            _obstaclesBypassForCreatureService = obstaclesBypassForCreatureService;
        }

        protected Vector2 CalculateBypassDirection()
        {
            if (_obstaclesBypassForCreatureService == null)
                return Vector2.zero;
            
            return _obstaclesBypassForCreatureService.CalculateBypassDirection();
        }
    }
}