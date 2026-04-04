using Core.Input.Interfaces;
using Models.Creatures.Dtos;
using Models.Creatures.Services.MoveBoosters.Enums;
using Models.Creatures.Services.MoveBoosters.Interfaces;
using Models.Creatures.Services.StatsScalers.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.MoveBoosters.Factories
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
            CreatureStats stats,
            ICreatureStatsScaler statsScaler)
        {
            return creatureMoveBoostType switch
            {
                CreatureMoveBoostType.ByInput =>
                    new ByInputCreatureMoveBooster(rigidbody2D, stats, statsScaler, _inputHandler),
                _ => (ICreatureMoveBooster)null
            };
        }
    }
}