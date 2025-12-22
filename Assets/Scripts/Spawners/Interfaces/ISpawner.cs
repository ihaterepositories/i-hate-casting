using System.Collections;
using Systems.Pooling.Models;

namespace Spawners.Interfaces
{
    // Handles gameobjects instantiating behaviour.
    public interface ISpawner<T> where T : PoolableMonoBehaviour
    {
        public IEnumerator LaunchSpawning();
    }
}