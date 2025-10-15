using Models.WorldObjects.Creatures.Base.StatsHandling.Enums;

namespace Models.WorldObjects.Creatures.Base.StatsHandling.Fabrics
{
    public class CreatureStatsMultiplierFactory
    {
        private readonly CreatureStatsMultiplier _playerStatsMultiplier;
        private readonly CreatureStatsMultiplier _enemiesStatsMultiplier;
        private readonly CreatureStatsMultiplier _bossesStatsMultiplier;

        private CreatureStatsMultiplierFactory()
        {
            _playerStatsMultiplier = new CreatureStatsMultiplier();
            _enemiesStatsMultiplier = new CreatureStatsMultiplier();
            _bossesStatsMultiplier = new CreatureStatsMultiplier();
        }

        public CreatureStatsMultiplier GetFor(CreatureType creatureType)
        {
            return creatureType switch
            {
                CreatureType.Player => _playerStatsMultiplier,
                CreatureType.Enemy => _enemiesStatsMultiplier,
                CreatureType.Boss => _bossesStatsMultiplier,
                _ => null
            };
        }
    }
}