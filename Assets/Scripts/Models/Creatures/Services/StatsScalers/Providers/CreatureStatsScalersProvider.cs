using System;
using Models.Creatures.Enums;
using Models.Creatures.Services.StatsScalers.Interfaces;

namespace Models.Creatures.Services.StatsScalers.Providers
{
    public class CreatureStatsScalersProvider
    {
        private readonly ICreatureStatsScaler _playerStatsScaler;
        private readonly ICreatureStatsScaler _enemiesStatsScaler;
        private readonly ICreatureStatsScaler _bossesStatsScaler;

        public CreatureStatsScalersProvider()
        {
            _playerStatsScaler = new CreatureStatsScaler();
            _enemiesStatsScaler = new CreatureStatsScaler();
            _bossesStatsScaler = new CreatureStatsScaler();
        }

        public ICreatureStatsScaler GetFor(CreatureType creatureType)
        {
            return creatureType switch
            {
                CreatureType.Player => _playerStatsScaler,
                CreatureType.Enemy => _enemiesStatsScaler,
                CreatureType.Boss => _bossesStatsScaler,
                _ => throw new ArgumentOutOfRangeException(nameof(creatureType), creatureType, null)
            };
        }
    }
}