using Core.Input.Interfaces;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Enums;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Interfaces;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.MoveBoosting.Factories
{
    public class MoveBoostersFactory
    {
        private readonly IInputHandler _inputHandler;
        
        public MoveBoostersFactory(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        public IMoveBoostService Create(
            MoveBoostType moveBoostType, 
            Rigidbody2D rigidbody2D, 
            CreatureStatsCalculator creatureStatsCalculator)
        {
            return moveBoostType switch
            {
                MoveBoostType.ByInput =>
                    new ByInputMoveBooster(rigidbody2D, creatureStatsCalculator, _inputHandler),
                _ => (IMoveBoostService)null
            };
        }
    }
}