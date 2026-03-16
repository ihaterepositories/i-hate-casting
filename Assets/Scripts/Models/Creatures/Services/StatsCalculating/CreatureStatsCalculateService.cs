using Models.Creatures.Dtos;
using Models.Creatures.Services.StatsCalculating.Interfaces;
using Models.Creatures.Services.StatsMultiplying.Interfaces;

namespace Models.Creatures.Services.StatsCalculating
{
    public class CreatureStatsCalculateService : ICreatureStatsCalculator
    {
        private readonly CreatureStats _baseStats;
        private readonly ICreatureStatsMultipliers _statsMultipliers;

        public CreatureStatsCalculateService(CreatureStats stats, ICreatureStatsMultipliers statsMultipliers)
        {
            _baseStats = stats;
            _statsMultipliers = statsMultipliers;
        }
        
        public float CalculateMaxHealth() => _statsMultipliers.ModifyMaxHealth(_baseStats.MaxHealth);
        
        public float CalculateSpeed() => _statsMultipliers.ModifySpeed(_baseStats.Speed);

        public float CalculateBoostStrength() => _statsMultipliers.ModifyBoostStrength(_baseStats.BoostStrength);
        
        public float CalculateBoostDuration() => _statsMultipliers.ModifyBoostDuration(_baseStats.BoostDuration);

        public float CalculateBoostCooldownTime() => _statsMultipliers.ModifyBoostCooldownTime(_baseStats.BoostCooldownTime);
    }
}