using Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Constants;
using Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Interfaces;
using Models.WorldObjects.Creatures.PlayerImpl;
using Models.WorldObjects.Creatures.PlayerImpl.DataContainers;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing
{
    public class ObstaclesBypasser : IObstaclesBypassService
    {
        private Transform _transform;
        private ObstaclesBypassConfig _config;

        private PlayerPositionTracker _playerPositionTracker;
        
        private Collider2D[] _nearestFoundColliders = new Collider2D[16];

        public ObstaclesBypasser(
            Transform transform,
            ObstaclesBypassConfig config,
            PlayerPositionTracker playerPositionTracker)
        {
            _transform = transform;
            _config = config;
            _playerPositionTracker = playerPositionTracker;
        }
        
        public Vector2 CalculateBypassDirection()
        {
            if (_config.DetectionRadius <= 0 || _config.BypassStrength <= 0) return Vector2.zero;
            
            // Fixes the issue when enemies are stuck together near the player and can`t damage him
            var distanceToPlayer = Vector2.Distance(_transform.position, _playerPositionTracker.Position);
            if (distanceToPlayer < _config.DistanceToPlayerWhenStopBypassing) return Vector2.zero;
            //
            
            int nearestCollidersCount = Physics2D.OverlapCircleNonAlloc(_transform.position, _config.DetectionRadius, _nearestFoundColliders);
            Vector2 separatedFromAllNearestEnemiesDirection = Vector2.zero;

            for (int i = 0; i < nearestCollidersCount; i++)
            {
                var currentCollider = _nearestFoundColliders[i];

                if (currentCollider.TryGetComponent<Player>(out _)) continue;
                if (currentCollider.TryGetComponent<Creature>(out var otherCreature) && otherCreature.transform.position == _transform.position) continue;
                
                // Represents the movement slightly to the side from the other creature
                Vector2 difference = (Vector2)(_transform.position - currentCollider.transform.position);
                    
                float distanceToOtherEnemy = difference.magnitude;

                if (distanceToOtherEnemy > 0)
                {
                    // The closer the creature, the higher the strength
                    float strength = Mathf.Clamp01(1 - distanceToOtherEnemy / _config.DetectionRadius);
                        
                    // Takes into account the previous calculated separation directions
                    // to create a balanced final direction
                    separatedFromAllNearestEnemiesDirection += difference.normalized * strength;
                }
            }

            return separatedFromAllNearestEnemiesDirection * _config.BypassStrength;
        }
    }
}