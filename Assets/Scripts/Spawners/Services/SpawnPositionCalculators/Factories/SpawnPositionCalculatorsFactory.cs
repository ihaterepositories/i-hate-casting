using System;
using Spawners.Services.SpawnPositionCalculators.Dtos;
using Spawners.Services.SpawnPositionCalculators.Enums;
using Spawners.Services.SpawnPositionCalculators.Interfaces;

namespace Spawners.Services.SpawnPositionCalculators.Factories
{
    public class SpawnPositionCalculatorsFactory
    {
        private readonly SafeSpawnSettings _safeSpawnSettings;

        public SpawnPositionCalculatorsFactory(SafeSpawnSettings safeSpawnSettings)
        {
            _safeSpawnSettings = safeSpawnSettings;
        }

        public ISpawnPositionCalculator Create(SpawnPositionType spawnPositionType)
        {
            return spawnPositionType switch
            {
                SpawnPositionType.Center => new CenterSpawnPositionCalculator(),
                SpawnPositionType.SafeRandom => new SafeRandomSpawnPositionCalculator(_safeSpawnSettings),
                _ => throw new ArgumentOutOfRangeException(nameof(spawnPositionType), spawnPositionType, null)
            };
        }
    }
}