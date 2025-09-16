using Models.Creatures.Implementations.PlayerImplementation;
using Models.Pooling;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Base.Spawning
{
    /// <summary>
    /// Parent class for creatures spawners, can only spawn objects.
    /// Implement spawn strategy in the child class.
    /// </summary>
    /// <typeparam name="T">Specific creature (inherited from creature) to spawn.</typeparam>
    public class CreaturesSpawner<T> : MonoBehaviour where T : Creature
    {
        [Header("Dependencies")]
        [SerializeField] private PoolContainer<T> _creaturesPool;
        
        [Header("Settings")]
        [SerializeField] private int _maxEnemiesCountSpawnedByThisSpawner;
        [SerializeField] private Vector2 _spawnRadius;
        [SerializeField] private float _minDistanceFromPlayerToSpawn;
        [SerializeField] private float _minDistanceFromOtherObjectsToSpawn;
        
        private Player _player;
        private int _currentEnemiesCountSpawnedByThisSpawner;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }
        
        protected void Spawn()
        {
            if (_currentEnemiesCountSpawnedByThisSpawner >= _maxEnemiesCountSpawnedByThisSpawner) return;
            var creature = _creaturesPool.GetFreeObject();
            creature.transform.position = GetFreePosition();
            creature.OnDeath += () => _currentEnemiesCountSpawnedByThisSpawner--;
            _currentEnemiesCountSpawnedByThisSpawner++;
        }
        
        private Vector2 GetFreePosition()
        {
            var newPosition = new Vector2(Random.Range(-_spawnRadius.x, _spawnRadius.x),
                Random.Range(-_spawnRadius.y, _spawnRadius.y));

            if (Vector2.Distance(newPosition, _player.transform.position) < _minDistanceFromPlayerToSpawn)
                return GetFreePosition();

            var nearestColliders = new Collider2D[15];
            if (Physics2D.OverlapCircleNonAlloc(newPosition, _minDistanceFromOtherObjectsToSpawn, nearestColliders) > 0)
                return GetFreePosition();
            
            return newPosition;
        }
    }
}