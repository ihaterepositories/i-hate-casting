using System;
using UnityEngine;
using Zenject;

namespace Models.Spawning
{
    public class GameObjectsSpawner : MonoBehaviour
    {
        private DiContainer _diContainer;
        
        public event Action<GameObject> OnSpawned;
        
        [Inject]
        private void Construct(DiContainer container)
        {
            _diContainer = container;
        }
        
        /// <summary>
        /// Spawns the given prefab as a child of the specified parent transform.
        /// Returns the spawned GameObject via the onSpawnedCallback if needed.
        /// </summary>
        /// <param name="prefabToSpawn">Prefab which will be spawned.</param>
        /// <param name="parent">Parent transform where prefabToSpawn will be spawned as a child.</param>
        /// <param name="onReturnSpawnedObjectCallback">Returns spawned GameObject.</param>
        public void Spawn(GameObject prefabToSpawn, Transform parent, Action<GameObject> onReturnSpawnedObjectCallback = null)
        {
            if (prefabToSpawn != null)
            {
                var prefab = _diContainer.InstantiatePrefab(prefabToSpawn, parent);
                prefab.transform.localPosition = Vector3.zero;
                onReturnSpawnedObjectCallback?.Invoke(prefab);
                OnSpawned?.Invoke(prefab);
            }
            else
            {
                Debug.LogError($"Received prefab to spawn is null.");
            }
        }
    }
}