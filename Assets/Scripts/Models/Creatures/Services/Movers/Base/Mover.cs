using Models.Creatures.Dtos;
using Models.Creatures.Services.StatsScalers.Interfaces;
using Shared.Services.ObstaclesBypassCalculators.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.Movers.Base
{
    public class Mover
    {
        protected readonly Rigidbody2D _rb;
        protected readonly ICreatureStatsScaler _statsScaler;
        protected readonly CreatureStats _stats;
     
        private IObstaclesBypassCalculator _obstaclesBypassCalculator;
        
        public bool IsMoving => _rb.linearVelocity.magnitude > 0.01f;
        public bool IsDirectedToTheRight => _rb.linearVelocity.x > 0;
        
        protected Mover(
            Rigidbody2D rb,
            CreatureStats stats,
            ICreatureStatsScaler statsScaler)
        {
            _rb = rb;
            _stats = stats;
            _statsScaler = statsScaler;
        }
        
        public void AssignObstaclesBypasser(IObstaclesBypassCalculator obstaclesBypassCalculator)
        {
            _obstaclesBypassCalculator = obstaclesBypassCalculator;
        }

        protected Vector2 CalculateBypassDirection()
        {
            if (_obstaclesBypassCalculator == null)
                return Vector2.zero;
            
            return _obstaclesBypassCalculator.CalculateBypassDirection();
        }
    }
}