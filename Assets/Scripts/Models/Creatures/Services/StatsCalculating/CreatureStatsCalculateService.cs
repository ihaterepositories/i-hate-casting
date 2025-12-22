using Models.Creatures.Dtos;
using Models.Creatures.Services.StatsCalculating.Interfaces;
using Models.Creatures.Services.StatsCalculating.StatsModifying.Interfaces;

namespace Models.Creatures.Services.StatsCalculating
{
    public class CreatureStatsCalculateService : ICreatureStatsCalculator
    {
        private readonly CreatureStats _baseStats;
        private readonly ICreatureStatsModifier _statsModifier;

        public CreatureStatsCalculateService(CreatureStats stats, ICreatureStatsModifier statsModifier)
        {
            _baseStats = stats;
            _statsModifier = statsModifier;
        }
        
        public float CalculateMaxHealth() => _statsModifier.ModifyMaxHealth(_baseStats.MaxHealth);
        
        public float CalculateSpeed() => _statsModifier.ModifySpeed(_baseStats.Speed);

        public float CalculateBoostStrength() => _statsModifier.ModifyBoostStrength(_baseStats.BoostStrength);
        
        public float CalculateBoostDuration() => _statsModifier.ModifyBoostDuration(_baseStats.BoostDuration);

        public float CalculateBoostCooldownTime() => _statsModifier.ModifyBoostCooldownTime(_baseStats.BoostCooldownTime);
    }
}