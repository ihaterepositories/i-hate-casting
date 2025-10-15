using Core.Input.Interfaces;
using Models.WorldObjects.Creatures.Base.Moving.Enums;
using Models.WorldObjects.Creatures.Base.Moving.Interfaces;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using Models.WorldObjects.Creatures.PlayerImpl;
using Models.WorldObjects.Creatures.PlayerImpl.DataContainers;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.Moving.Fabrics
{
    public class MoversFabric
    {
        private readonly PlayerPositionTracker _playerPositionTracker;
        private readonly IInputHandler _inputHandler;
        
        public MoversFabric(
            PlayerPositionTracker playerPositionTracker,
            IInputHandler inputHandler)
        {
            _playerPositionTracker = playerPositionTracker;
            _inputHandler = inputHandler;
        }

        public IMoveService Create(
            MoveType moveType, 
            Rigidbody2D rigidbody2D, Transform transform,
            CreatureStatsCalculator statsCalculator)
        {
            switch (moveType)
            {
                case MoveType.ByInput:
                    return new ByInputMover(rigidbody2D, statsCalculator, _inputHandler);
                case MoveType.PlayerFollowing:
                    return new FollowingPlayerMover(rigidbody2D, transform, statsCalculator, _playerPositionTracker);
                default:
                    return null;
            }
        }
    }
}