using System;
using Models.Creatures.Services.MoveBoosting.Interfaces;
using Models.UI.CooldownViewBars.Services.ValueProviding.Interfaces;

namespace Models.UI.CooldownViewBars.Services.ValueProviding
{
    public class PlayerBoostCooldownBarValueProvider : ICooldownBarValueProvider
    {
        private readonly ICreatureMoveBooster _creatureMoveBoostService;

        public PlayerBoostCooldownBarValueProvider(ICreatureMoveBooster creatureMoveBoostService)
        {
            _creatureMoveBoostService = creatureMoveBoostService;
        }
        
        public Tuple<float, float> GetValue()
        {
            return new Tuple<float, float>(
                _creatureMoveBoostService.BoostCooldownTimeElapsed,
                _creatureMoveBoostService.BoostCooldownDuration);
        }
    }
}