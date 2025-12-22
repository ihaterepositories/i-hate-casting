using System.Collections;
using Spawners.Interfaces;
using Spawners.Services.Instantiaters.Interfaces;
using Spawners.Services.SpawnBehaviourProviders.Interfaces;
using Spawners.Services.SpawnPositionCalculators.Interfaces;
using Systems.Pooling.Models;

namespace Spawners
{
    // Ideas to implement (if it will be needed):
    // - spawn limit
    // - stop/continue spawning
    // - list of spawned objects (currently alive)
    public class Spawner<T> : ISpawner<T> where T : PoolableMonoBehaviour
    {
        private readonly IInstantiater<T> _instantiater;
        private readonly ISpawnPositionCalculator _spawnPositionCalculator;
        private readonly ISpawnBehaviourExecutor _spawnBehaviourExecutor;
        
        private readonly float _delayBeforeNextSpawn;

        public Spawner(
            IInstantiater<T> instantiater,
            ISpawnPositionCalculator spawnPositionCalculator,
            ISpawnBehaviourExecutor spawnBehaviourExecutor,
            float delayBeforeNextSpawn)
        {
            _instantiater = instantiater;
            _spawnPositionCalculator = spawnPositionCalculator;
            _spawnBehaviourExecutor = spawnBehaviourExecutor;
            _delayBeforeNextSpawn = delayBeforeNextSpawn;
        }

        public IEnumerator LaunchSpawning()
        {
            yield return _spawnBehaviourExecutor.Launch(SpawnObject, _delayBeforeNextSpawn);
        }

        private void SpawnObject()
        {
            var instance = _instantiater.Create();
            instance.transform.position = _spawnPositionCalculator.GetSpawnPosition();
        }
    }
}