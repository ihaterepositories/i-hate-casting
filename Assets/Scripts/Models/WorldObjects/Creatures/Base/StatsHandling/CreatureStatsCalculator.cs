using Models.WorldObjects.Creatures.Base.StatsHandling.DataContainers;
using Models.WorldObjects.Creatures.Base.StatsHandling.Enums;

namespace Models.WorldObjects.Creatures.Base.StatsHandling
{
    /// <summary>
    /// Util class, calculates creature stats based on base stats and stats multiplying.
    /// </summary>
    public class CreatureStatsCalculator
    {
        private readonly CreatureStats _baseCreatureStats;
        private readonly CreatureStatsMultiplier _creatureStatsMultiplier;

        public CreatureStatsCalculator(CreatureStats creatureStats, CreatureStatsMultiplier creatureStatsMultiplier)
        {
            _baseCreatureStats = creatureStats;
            _creatureStatsMultiplier = creatureStatsMultiplier;
        }
        
        public float GetMaxHealth() => 
            _baseCreatureStats.MaxHealth * _creatureStatsMultiplier.Multiply(CreatureStatType.MaxHealth);
        
        public float GetSpeed() =>
            _baseCreatureStats.Speed * _creatureStatsMultiplier.Multiply(CreatureStatType.Speed);
        
        public float GetMoveBoostDuration() => 
            _baseCreatureStats.BurstDuration * _creatureStatsMultiplier.Multiply(CreatureStatType.BurstDuration);
        
        public float GetMoveBoostCoefficient() => 
            _baseCreatureStats.WhileBurstSpeedIncreaseCoefficient * _creatureStatsMultiplier.Multiply(CreatureStatType.WhileBurstSpeedIncreaseCoefficient);
        
        public float GetMoveBoostCooldownTime() =>
            _baseCreatureStats.BurstCooldownTime * _creatureStatsMultiplier.Multiply(CreatureStatType.BurstCooldown);
    }
}