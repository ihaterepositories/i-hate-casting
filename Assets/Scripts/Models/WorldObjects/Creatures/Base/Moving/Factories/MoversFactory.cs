using Core.Input.Interfaces;
using Models.WorldObjects.Creatures.Base.Moving.Enums;
using Models.WorldObjects.Creatures.Base.Moving.Interfaces;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using Models.WorldObjects.Creatures.PlayerImpl.DataContainers;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.Moving.Factories
{
    public class MoversFactory
    {
        private readonly PlayerPositionTracker _playerPositionTracker;
        private readonly IInputHandler _inputHandler;
        
        public MoversFactory(
            PlayerPositionTracker playerPositionTracker,
            IInputHandler inputHandler)
        {
            _playerPositionTracker = playerPositionTracker;
            _inputHandler = inputHandler;
        }

        public IMoveService Create(
            MoveType moveType, 
            CreatureStatsCalculator statsCalculator,
            Rigidbody2D rigidbody2D, 
            Transform transform)
        {
            return moveType switch
            {
                MoveType.ByInput => new ByInputMover(statsCalculator, rigidbody2D, _inputHandler),
                MoveType.PlayerFollowing => new FollowingPlayerMover(statsCalculator, rigidbody2D, transform,
                    _playerPositionTracker),
                _ => (IMoveService)null
            };
        }
    }
}