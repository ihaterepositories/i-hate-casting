using Models.WorldObjects.Base.Pooling;
using Models.WorldObjects.Base.Spawning.Utils;
using Models.WorldObjects.Creatures.PlayerImpl;
using UnityEngine;
using Zenject;

namespace Models.WorldObjects.Base.Spawning
{
    /// <summary>
    /// Parent class for world objects spawners, can only spawn objects.
    /// Implement spawn strategy in the child class.
    /// </summary>
    /// <typeparam name="T">Poolable game object.</typeparam>
    public class WorldObjectsSpawner<T> : MonoBehaviour where T : PoolableMonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private PoolContainer<T> _pool;
        
        private int _currentObjectsCountSpawned;
        private SafeSpawnProvider _safeSpawnProvider;
        
        public int CurrentObjectsCountSpawned => _currentObjectsCountSpawned;

        [Inject]
        private void Construct(Player player)
        {
            _safeSpawnProvider = new SafeSpawnProvider(player, false);
        }

        protected void Spawn()
        {
            var worldObject = _pool.GetFreeObject();
            worldObject.transform.position = _safeSpawnProvider.GetSafeSpawnPosition();
            worldObject.OnDestroyed += () => _currentObjectsCountSpawned--;
            _currentObjectsCountSpawned++;
        }
    }
}