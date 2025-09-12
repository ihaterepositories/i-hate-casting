using Models.Creatures.Base.StatsHandling.Enums;
using Models.Creatures.Base.StatsHandling.ScriptableObjects;

namespace Models.Creatures.Base.StatsHandling
{
    /// <summary>
    /// Util class, calculates creature stats based on base stats and stats multiplying.
    /// </summary>
    public class CreatureStatsCalculator
    {
        private readonly CreatureStatsSo _baseCreatureStats;
        private readonly CreatureStatsMultiplier _creatureStatsMultiplier;

        public CreatureStatsCalculator(CreatureStatsSo creatureStatsSo, CreatureStatsMultiplier creatureStatsMultiplier)
        {
            _baseCreatureStats = creatureStatsSo;
            _creatureStatsMultiplier = creatureStatsMultiplier;
        }
        
        public float GetMaxHealth() => 
            _baseCreatureStats.MaxHealth * _creatureStatsMultiplier.GetMultiplier(CreatureStatType.MaxHealth);
        
        public float GetSpeed() =>
            _baseCreatureStats.Speed * _creatureStatsMultiplier.GetMultiplier(CreatureStatType.Speed);
        
        public float GetBurstDuration() => 
            _baseCreatureStats.BurstDuration * _creatureStatsMultiplier.GetMultiplier(CreatureStatType.BurstDuration);
        
        public float GetWhileBurstSpeedIncreaseCoefficient() => 
            _baseCreatureStats.WhileBurstSpeedIncreaseCoefficient * _creatureStatsMultiplier.GetMultiplier(CreatureStatType.WhileBurstSpeedIncreaseCoefficient);
        
        public float GetBurstCooldownTime() =>
            _baseCreatureStats.BurstCooldownTime * _creatureStatsMultiplier.GetMultiplier(CreatureStatType.BurstCooldown);
    }
}