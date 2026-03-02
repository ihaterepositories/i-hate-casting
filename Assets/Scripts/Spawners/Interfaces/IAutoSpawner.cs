using System;
using System.Collections;
using Systems.Pooling.Models;

namespace Spawners.Interfaces
{
    // Handles gameobjects instantiating behaviour.
    public interface IAutoSpawner<T> where T : PoolableMonoBehaviour
    {
        /// <summary>
        /// Returns last spawned object.
        /// </summary>
        public event Action<T> OnObjectSpawned;
        
        /// <summary>
        /// Starts spawning with passed settings during spawner creation.
        /// </summary>
        /// <returns></returns>
        public IEnumerator LaunchSpawning();
    }
}