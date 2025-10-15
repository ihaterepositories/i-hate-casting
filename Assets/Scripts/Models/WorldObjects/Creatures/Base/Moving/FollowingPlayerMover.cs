using Models.WorldObjects.Creatures.Base.Moving.Base;
using Models.WorldObjects.Creatures.Base.Moving.Interfaces;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using Models.WorldObjects.Creatures.PlayerImpl.DataContainers;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.Moving
{
    public class FollowingPlayerMover : Mover, IMoveService
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Transform _transform;
        private readonly CreatureStatsCalculator _statsCalculator;
        private readonly PlayerPositionTracker _playerPositionTracker;
        
        public FollowingPlayerMover(
            Rigidbody2D rigidbody2D,
            Transform transform,
            CreatureStatsCalculator statsCalculator,
            PlayerPositionTracker playerPositionTracker)
        {
            _rigidbody2D = rigidbody2D;
            _transform = transform;
            _statsCalculator = statsCalculator;
            _playerPositionTracker = playerPositionTracker;
        }

        public void Move()
        {
            Vector2 moveDirection = CalculateDirectionToPlayerVector();
            Vector2 bypassDirection = CalculateBypassDirection();

            Vector2 finalDirection = (moveDirection + bypassDirection).normalized;

            Vector2 newVelocity = finalDirection * _statsCalculator.GetSpeed();

            _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, newVelocity, Time.fixedDeltaTime * 5f);
        }

        private Vector2 CalculateDirectionToPlayerVector()
        {
            return (_playerPositionTracker.Position - _transform.position).normalized;
        }
    }
}