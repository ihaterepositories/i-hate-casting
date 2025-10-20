using Models.WorldObjects.Creatures.Base.Moving.Base;
using Models.WorldObjects.Creatures.Base.Moving.Interfaces;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using Models.WorldObjects.Creatures.PlayerImpl.DataContainers;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.Moving
{
    public class FollowingPlayerMover : Mover, IMoveService
    {
        private readonly Transform _transform;
        private readonly PlayerPositionTracker _playerPositionTracker;
        
        public FollowingPlayerMover(
            CreatureStatsCalculator statsCalculator, 
            Rigidbody2D rb, 
            Transform transform,
            PlayerPositionTracker playerPositionTracker) : base(statsCalculator, rb)
        {
            _transform = transform;
            _playerPositionTracker = playerPositionTracker;
        }

        public void EnableMove()
        {
            Vector2 moveDirection = CalculateDirectionToPlayerVector();
            Vector2 bypassDirection = CalculateBypassDirection();

            Vector2 finalDirection = (moveDirection + bypassDirection).normalized;

            Vector2 newVelocity = finalDirection * _statsCalculator.GetSpeed();

            _rb.velocity = Vector2.Lerp(_rb.velocity, newVelocity, Time.fixedDeltaTime * 5f);
        }

        private Vector2 CalculateDirectionToPlayerVector()
        {
            return (_playerPositionTracker.Position - _transform.position).normalized;
        }
    }
}