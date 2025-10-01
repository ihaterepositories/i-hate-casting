using Models.Creatures.PlayerImpl;
using UnityEngine;
using Zenject;

namespace Models.Creatures.EnemyImpl.Movers
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Enemy _enemy;
        
        [Header("Obstacles avoiding settings")] 
        [SerializeField] private float _obstaclesDetectionRadius = 1.5f;
        [SerializeField] private float _obstaclesAvoidStrength = 2f;

        private Player _player;
        private Collider2D[] _nearestFoundColliders = new Collider2D[16];

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }

        private void FixedUpdate()
        {
            ChangeVelocity();
        }

        private void ChangeVelocity()
        {
            Vector2 moveDir = CalculateDirectionToPlayerVector();
            Vector2 separation = CalculateSeparationVector();

            Vector2 finalDir = (moveDir + separation).normalized;

            Vector2 newVelocity = finalDir * _enemy.StatsCalculator.GetSpeed();

            _rb.velocity = Vector2.Lerp(_rb.velocity, newVelocity, Time.fixedDeltaTime * 5f);
        }

        private Vector2 CalculateDirectionToPlayerVector()
        {
            return (_player.transform.position - transform.position).normalized;
        }

        private Vector2 CalculateSeparationVector()
        {
            // Fixes the issue when enemies are stuck together near the player and can`t damage him
            var distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
            if (distanceToPlayer < 1f) return Vector2.zero;
            //
            
            int nearestCollidersCount = Physics2D.OverlapCircleNonAlloc(transform.position, _obstaclesDetectionRadius, _nearestFoundColliders);
            Vector2 separatedFromAllNearestEnemiesDirection = Vector2.zero;

            for (int i = 0; i < nearestCollidersCount; i++)
            {
                var currentCollider = _nearestFoundColliders[i];

                if (currentCollider.TryGetComponent<Player>(out _)) continue;
                if (currentCollider.TryGetComponent<Enemy>(out var otherEnemy) && otherEnemy == _enemy) continue;
                
                // Represents the movement slightly to the side from the other enemy
                Vector2 difference = (Vector2)(transform.position - currentCollider.transform.position);
                    
                float distanceToOtherEnemy = difference.magnitude;

                if (distanceToOtherEnemy > 0)
                {
                    // The closer the enemy, the higher the strength
                    float strength = Mathf.Clamp01(1 - distanceToOtherEnemy / _obstaclesDetectionRadius);
                        
                    // Takes into account the previous calculated separation directions
                    // to create a balanced final direction
                    separatedFromAllNearestEnemiesDirection += difference.normalized * strength;
                }
            }

            return separatedFromAllNearestEnemiesDirection * _obstaclesAvoidStrength;
        }
    }
}