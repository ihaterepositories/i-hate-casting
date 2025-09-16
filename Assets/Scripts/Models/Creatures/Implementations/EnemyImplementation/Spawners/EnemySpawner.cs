using System.Collections;
using Models.Creatures.Base.Spawning;
using Models.Creatures.Implementations.EnemyImplementation.Pools;
using Models.Creatures.Implementations.PlayerImplementation;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Implementations.EnemyImplementation.Spawners
{
    /// <summary>
    /// Spawns enemies every certain time interval.
    /// </summary>
    public class EnemySpawner : CreaturesSpawner<Enemy>
    {
        [SerializeField] private float _spawnInterval;
        
        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }
        
        // TODO: Implement spawn stop when game over and maybe smth else
        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnInterval);
                Spawn();
            }
        }
    }
}