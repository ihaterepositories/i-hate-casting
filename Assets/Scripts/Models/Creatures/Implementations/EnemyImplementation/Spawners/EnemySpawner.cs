using System.Collections;
using Models.Creatures.Base.Pooling;
using UnityEngine;

namespace Models.Creatures.Implementations.EnemyImplementation.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Pool for the specific type of enemy this spawner are going to spawn")]
        [SerializeField] private CreaturesPool _enemiesPool;
        
        [Header("Settings")]
        [SerializeField] private int _maxEnemiesCountSpawnedByThisSpawner;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private Vector2 _spawnRadius;

        private int _currentEnemiesCountSpawnedByThisSpawner;
        
        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }
        
        // TODO: Implement spawn stop when game over and maybe smth else
        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(_spawnInterval);

            if (_currentEnemiesCountSpawnedByThisSpawner <= _maxEnemiesCountSpawnedByThisSpawner)
            {
                var enemy = _enemiesPool.GetFreeObject();
                enemy.transform.position = 
                    new Vector2(Random.Range(-_spawnRadius.x, _spawnRadius.x), Random.Range(-_spawnRadius.y, _spawnRadius.y));
                
                enemy.OnDeath += () => _currentEnemiesCountSpawnedByThisSpawner--;
                
                _currentEnemiesCountSpawnedByThisSpawner++;
            }
            
            StartCoroutine(SpawnCoroutine());
        }
    }
}