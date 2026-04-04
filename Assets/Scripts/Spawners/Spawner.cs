using Shared.Models.PoolableMonoBehaviours;
using Spawners.Interfaces;
using Spawners.Services.Instantiaters.Interfaces;

namespace Spawners
{
    public class Spawner<T> : ISpawner<T> where T : PoolableMonoBehaviour
    {
        private IInstantiater<T> _instantiater;

        public Spawner(IInstantiater<T> instantiater)
        {
            _instantiater = instantiater;
        }
        
        public T Spawn()
        {
            var instance = _instantiater.Create();
            return instance;
        }
    }
}