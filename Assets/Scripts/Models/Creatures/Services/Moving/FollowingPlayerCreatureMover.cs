using Models.Creatures.Services.Moving.Base;
using Models.Creatures.Services.Moving.Interfaces;
using Models.Creatures.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.Moving
{
    public class FollowingPlayerCreatureMover : Mover, ICreatureMover
    {
        private readonly Transform _transform;
        private readonly Transform _playerTransform;
        
        public FollowingPlayerCreatureMover(
            ICreatureStatsCalculator statsCalculateService, 
            Rigidbody2D rb, 
            Transform transform,
            Transform playerTransform) : base(statsCalculateService, rb)
        {
            _transform = transform;
            _playerTransform = playerTransform;
        }

        public void EnableMove()
        {
            Vector2 moveDirection = CalculateDirectionToPlayerVector();
            Vector2 bypassDirection = CalculateBypassDirection();

            Vector2 finalDirection = (moveDirection + bypassDirection).normalized;

            Vector2 newVelocity = finalDirection * _statsCalculateService.CalculateSpeed();

            _rb.velocity = Vector2.Lerp(_rb.velocity, newVelocity, Time.fixedDeltaTime * 5f);
        }

        private Vector2 CalculateDirectionToPlayerVector()
        {
            return (_playerTransform.position - _transform.position).normalized;
        }
    }
}