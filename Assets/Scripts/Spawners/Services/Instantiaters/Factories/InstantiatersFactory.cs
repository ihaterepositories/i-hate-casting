using System;
using Pooling.Factories;
using Spawners.Services.Instantiaters.Enums;
using Spawners.Services.Instantiaters.Interfaces;
using Systems.Pooling.Models;
using UnityEngine;
using Zenject;

namespace Spawners.Services.Instantiaters.Factories
{
    public class InstantiatersFactory
    {
        private readonly DiContainer _diContainer;
        private readonly ObjectPoolsFactory _objectPoolsFactory;

        public InstantiatersFactory(DiContainer diContainer, ObjectPoolsFactory objectPoolsFactory)
        {
            _diContainer = diContainer;
            _objectPoolsFactory = objectPoolsFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instantiatingType"></param>
        /// <param name="prefab"></param>
        /// <typeparam name="T">Component of given prefab that Instantiater will be returning.</typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public IInstantiater<T> Create<T>(
            InstantiatingType instantiatingType,
            GameObject prefab) 
            where T : PoolableMonoBehaviour
        {
            return instantiatingType switch
            {
                InstantiatingType.Default => new DefaultInstantiater<T>(prefab, _diContainer),
                InstantiatingType.PoolBased => new PoolBasedInstantiater<T>(_objectPoolsFactory.CreateFor<T>(prefab)),
                _ => throw new ArgumentOutOfRangeException(nameof(instantiatingType), instantiatingType, null)
            };
        }
    }
}