using System;
using Spawners.Services.Instantiaters.Interfaces;
using Systems.Pooling.Models;
using UnityEngine;
using Zenject;

namespace Spawners.Services.Instantiaters
{
    public class DefaultInstantiater<T> : IInstantiater<T> where T : PoolableMonoBehaviour
    {
        private readonly DiContainer _diContainer;
        private readonly GameObject _prefab;

        public DefaultInstantiater(
            GameObject prefab,
            DiContainer diContainer)
        {
            _prefab = prefab;
            _diContainer = diContainer;
        }

        public T Create()
        {
            var go = _diContainer.InstantiatePrefabForComponent<T>(_prefab);
            
            if (go == null)
            {
                throw new Exception($"{_prefab.name} does not have {typeof(T).Name} component!");
            }
            
            return go;
        }
    }
}