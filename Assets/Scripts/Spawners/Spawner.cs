using System.Collections;
using Shared.Models.PoolableMonoBehaviours;
using Spawners.Interfaces;
using Spawners.Services.Instantiaters.Interfaces;
using Spawners.Services.SpawnPositionCalculators.Interfaces;
using UnityEngine;

namespace Spawners
{
    public class Spawner<T> : ISpawner<T> where T : PoolableMonoBehaviour
    {
        private IInstantiater<T> _instantiater;
        private ISpawnPositionCalculator _spawnPositionCalculator;

        public Spawner(
            IInstantiater<T> instantiater,
            ISpawnPositionCalculator spawnPositionCalculator)
        {
            _instantiater = instantiater;
            _spawnPositionCalculator = spawnPositionCalculator;
        }

        public void Spawn()
        {
            var instance = _instantiater.Create();
            instance.transform.position = _spawnPositionCalculator.GetSpawnPosition();
        }

        public T SpawnAndGet()
        {
            var instance = _instantiater.Create();
            instance.transform.position = _spawnPositionCalculator.GetSpawnPosition();
            return instance;
        }

        public IEnumerator SpawnCoroutine(float waitTime, bool continueSpawn)
        {
            yield return new WaitForSeconds(waitTime);
            
            Spawn();
            
            if (continueSpawn)
                yield return SpawnCoroutine(waitTime, true);
        }
    }
}