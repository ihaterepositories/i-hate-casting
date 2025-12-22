using System;
using Models.Creatures.Enums;
using Models.Creatures.Services.StatsCalculating.StatsModifying.Interfaces;

namespace Models.Creatures.Services.StatsCalculating.StatsModifying.Providers
{
    public class CreatureStatsModifiersProvider
    {
        private readonly ICreatureStatsModifier _playerStatsModifier;
        private readonly ICreatureStatsModifier _enemiesStatsModifier;

        public CreatureStatsModifiersProvider()
        {
            _playerStatsModifier = new CreatureStatsModifier();
            _enemiesStatsModifier = new CreatureStatsModifier();
        }

        public ICreatureStatsModifier GetFor(CreatureType creatureType)
        {
            return creatureType switch
            {
                CreatureType.Player => _playerStatsModifier,
                CreatureType.Enemy => _enemiesStatsModifier,
                _ => throw new ArgumentOutOfRangeException(nameof(creatureType), creatureType, null)
            };
        }
    }
}