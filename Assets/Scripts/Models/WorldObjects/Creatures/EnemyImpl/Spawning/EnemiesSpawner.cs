using System.Collections;
using Models.WorldObjects.Base.Spawning;
using UnityEngine;

namespace Models.WorldObjects.Creatures.EnemyImpl.Spawning
{
    /// <summary>
    /// Spawns enemies every certain time interval.
    /// </summary>
    public class EnemiesSpawner : WorldObjectsSpawner<Enemy>
    {
        [Header("Settings")]
        [SerializeField] private float _spawnInterval;
        [SerializeField] private int _maxEnemiesOnScene;
        
        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }
        
        // TODO: Implement spawn stop when game over and maybe smth else
        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                if (CurrentObjectsCountSpawned >= _maxEnemiesOnScene)
                {
                    yield return null;
                    continue;
                }
                
                yield return new WaitForSeconds(_spawnInterval);
                Spawn();
            }
        }
    }
}