using System;
using Spawners.Services.SpawnBehaviourProviders.Enums;
using Spawners.Services.SpawnBehaviourProviders.Interfaces;

namespace Spawners.Services.SpawnBehaviourProviders.Factories
{
    public class SpawnBehaviourProvidersFactory
    {
        public ISpawnBehaviourExecutor Create(SpawnBehaviourType spawnBehaviourType)
        {
            return spawnBehaviourType switch
            {
                SpawnBehaviourType.Cycled => new CycledSpawnBehaviourProvider(),
                SpawnBehaviourType.OneTimeSpawn => new OneTimeSpawnBehaviourProvider(),
                _ => throw new ArgumentOutOfRangeException(nameof(spawnBehaviourType), spawnBehaviourType, null)
            };
        }
    }
}