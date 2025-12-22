using Systems.Pooling.Models;

namespace Spawners.Services.Instantiaters.Interfaces
{
    public interface IInstantiater<T> where T : PoolableMonoBehaviour
    {
        public T Create();
    }
}