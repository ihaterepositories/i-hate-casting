using Core.Input.Interfaces;
using Models.Creatures.Services.MoveBoosting.Enums;
using Models.Creatures.Services.MoveBoosting.Interfaces;
using Models.Creatures.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.MoveBoosting.Factories
{
    public class CreatureMoveBoostersFactory
    {
        private readonly IInputHandler _inputHandler;
        
        public CreatureMoveBoostersFactory(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        public ICreatureMoveBooster Create(
            CreatureMoveBoostType creatureMoveBoostType, 
            Rigidbody2D rigidbody2D, 
            ICreatureStatsCalculator creatureStatsCalculateService)
        {
            return creatureMoveBoostType switch
            {
                CreatureMoveBoostType.ByInput =>
                    new ByInputCreatureMoveBooster(rigidbody2D, creatureStatsCalculateService, _inputHandler),
                _ => (ICreatureMoveBooster)null
            };
        }
    }
}