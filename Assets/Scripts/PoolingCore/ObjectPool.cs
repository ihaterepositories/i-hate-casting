using System;
using System.Collections.Generic;
using PoolingCore.Interfaces;
using UnityEngine;
using Zenject;

namespace PoolingCore
{
    public class ObjectPool<T> where T : Component, IPoolAble
    {
        private readonly List<IPoolAble> _freeObjects;
        private readonly Transform _container;
        private readonly T _prefab;
        private readonly DiContainer _diContainer;

        public ObjectPool(T prefab, DiContainer diContainer)
        {
            _freeObjects = new List<IPoolAble>();
            _container = new GameObject().transform;
            _container.name = prefab.GameObject.name;
            _prefab = prefab;
            
            _diContainer = diContainer ?? throw new ArgumentNullException(nameof(diContainer));
        }

        public IPoolAble GetFreeObject()
        {
            IPoolAble poolAble;

            if (_freeObjects.Count > 0)
            {
                poolAble = _freeObjects[0] as T;
                _freeObjects.RemoveAt(0);
            }
            else
            {
                GameObject instance = _diContainer.InstantiatePrefab(_prefab, _container);
                poolAble = instance.GetComponent<IPoolAble>();

                if (poolAble == null)
                    throw new Exception($"Instantiated object does not implement IPoolAble: {_prefab.name}");
            }

            if (poolAble == null)
            {
                throw new System.Exception("PoolAble object is null");
            }
            
            poolAble.GameObject.SetActive(true);
            poolAble.OnDestroyed += ReturnToPool;
        
            return poolAble;
        }

        private void ReturnToPool(IPoolAble poolAble)
        {
            _freeObjects.Add(poolAble);
            poolAble.OnDestroyed -= ReturnToPool;
            poolAble.GameObject.SetActive(false);
            poolAble.GameObject.transform.SetParent(_container);
        }
    }
}