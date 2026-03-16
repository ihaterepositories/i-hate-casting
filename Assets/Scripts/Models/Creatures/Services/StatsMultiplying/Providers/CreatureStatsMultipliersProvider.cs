using System;
using Models.Creatures.Enums;
using Models.Creatures.Services.StatsMultiplying.Interfaces;

namespace Models.Creatures.Services.StatsMultiplying.Providers
{
    public class CreatureStatsMultipliersProvider
    {
        private readonly ICreatureStatsMultipliers _playerStatsMultipliers;
        private readonly ICreatureStatsMultipliers _enemiesStatsMultipliers;

        public CreatureStatsMultipliersProvider()
        {
            _playerStatsMultipliers = new CreatureStatsMultipliers();
            _enemiesStatsMultipliers = new CreatureStatsMultipliers();
        }

        public ICreatureStatsMultipliers GetFor(CreatureType creatureType)
        {
            return creatureType switch
            {
                CreatureType.Player => _playerStatsMultipliers,
                CreatureType.Enemy => _enemiesStatsMultipliers,
                _ => throw new ArgumentOutOfRangeException(nameof(creatureType), creatureType, null)
            };
        }
    }
}