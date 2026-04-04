using ObjectPools;
using Shared.Models.PoolableMonoBehaviours;
using Spawners.Services.Instantiaters.Interfaces;

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