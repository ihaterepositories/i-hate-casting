using Models.Creatures.Dtos;
using Models.Creatures.Services.Movers.Base;
using Models.Creatures.Services.Movers.Interfaces;
using Models.Creatures.Services.StatsScalers.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.Movers
{
    public class FollowingPlayerCreatureMover : Mover, ICreatureMover
    {
        private readonly Transform _transform;
        private readonly Transform _playerTransform;
        
        public FollowingPlayerCreatureMover(
            Rigidbody2D rb,
            CreatureStats stats,
            ICreatureStatsScaler statsScaler, 
            Transform transform,
            Transform playerTransform) 
            : base(rb, stats, statsScaler)
        {
            _transform = transform;
            _playerTransform = playerTransform;
        }

        public void FixedTick()
        {
            Vector2 moveDirection = CalculateDirectionToPlayerVector();
            Vector2 bypassDirection = CalculateBypassDirection();

            Vector2 finalDirection = (moveDirection + bypassDirection).normalized;

            Vector2 newVelocity = finalDirection * _statsScaler.ScaleSpeed(_stats.Speed);

            _rb.linearVelocity = Vector2.Lerp(_rb.linearVelocity, newVelocity, Time.fixedDeltaTime * 5f);
        }

        private Vector2 CalculateDirectionToPlayerVector()
        {
            return (_playerTransform.position - _transform.position).normalized;
        }
    }
}