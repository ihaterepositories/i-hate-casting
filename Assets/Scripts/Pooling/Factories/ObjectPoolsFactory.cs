using Systems.Pooling.Models;
using UnityEngine;
using Zenject;

namespace Pooling.Factories
{
    public class ObjectPoolsFactory
    {
        private readonly DiContainer _diContainer;

        public ObjectPoolsFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public ObjectPool<T> CreateFor<T>(GameObject prefab) where T : PoolableMonoBehaviour
        {
            var parent = new GameObject($"{prefab.gameObject.name}sContainer");
            return new ObjectPool<T>(prefab, _diContainer, parent.transform);
        }
    }
}