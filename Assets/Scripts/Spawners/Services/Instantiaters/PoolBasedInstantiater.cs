using Pooling;
using Spawners.Services.Instantiaters.Interfaces;
using Systems.Pooling.Models;

namespace Spawners.Services.Instantiaters
{
    public class PoolBasedInstantiater<T> : IInstantiater<T> where T : PoolableMonoBehaviour
    {
        private readonly ObjectPool<T> _pool;

        public PoolBasedInstantiater(ObjectPool<T> pool)
        {
            _pool = pool;
        }
        
        public T Create()
        {
            return _pool.GetFreeObject();
        }
    }
}