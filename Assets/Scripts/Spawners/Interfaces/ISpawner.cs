using Shared.Models.PoolableMonoBehaviours;

namespace Spawners.Interfaces
{
    /// <summary>
    /// Spawns and returns game object. No auto spawn.
    /// </summary>
    public interface ISpawner<T> where T : PoolableMonoBehaviour
    {
        public T Spawn();
    }
}