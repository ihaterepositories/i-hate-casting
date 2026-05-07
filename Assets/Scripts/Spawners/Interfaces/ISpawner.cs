using System.Collections;
using Shared.Models.PoolableMonoBehaviours;

namespace Spawners.Interfaces
{
    /// <summary>
    /// Spawns game object in different provided ways.
    /// </summary>
    public interface ISpawner<T> where T : PoolableMonoBehaviour
    {
        public void Spawn();
        public T SpawnAndGet();
        public IEnumerator SpawnCoroutine(float waitTime, bool continueSpawn);
    }
}