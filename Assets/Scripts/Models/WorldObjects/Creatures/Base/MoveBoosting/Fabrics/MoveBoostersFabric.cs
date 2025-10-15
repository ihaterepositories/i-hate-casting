using Core.Input.Interfaces;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Enums;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Interfaces;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.MoveBoosting.Fabrics
{
    public class MoveBoostersFabric
    {
        private IInputHandler _inputHandler;
        
        public MoveBoostersFabric(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        public IMoveBoostService Create(
            MoveBoostType moveBoostType, 
            Rigidbody2D rigidbody2D, 
            CreatureStatsCalculator creatureStatsCalculator)
        {
            switch (moveBoostType)
            {
                default:
                case MoveBoostType.ByUserInput:
                    return new ByInputMoveBooster(rigidbody2D, creatureStatsCalculator, _inputHandler);
            }
        }
    }
}