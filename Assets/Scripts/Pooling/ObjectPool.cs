using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pooling
{
    public class ObjectPool<T> where T : PoolAbleMonoBehaviour
    {
        private readonly List<PoolAbleMonoBehaviour> _freeObjects;
        private readonly Transform _container;
        private readonly T _prefab;
        private readonly DiContainer _diContainer;

        public ObjectPool(T prefab, DiContainer diContainer, Transform parentForObjects)
        {
            _freeObjects = new List<PoolAbleMonoBehaviour>();
            _container = parentForObjects;
            _prefab = prefab;
            
            _diContainer = diContainer;
        }

        public PoolAbleMonoBehaviour GetFreeObject()
        {
            PoolAbleMonoBehaviour poolAble;

            if (_freeObjects.Count > 0)
            {
                poolAble = _freeObjects[0] as T;
                _freeObjects.RemoveAt(0);
            }
            else
            {
                GameObject instance = _diContainer.InstantiatePrefab(_prefab, _container);
                poolAble = instance.GetComponent<PoolAbleMonoBehaviour>();

                if (poolAble == null)
                    throw new Exception($"Instantiated object does not implement IPoolAble: {_prefab.name}");
            }

            if (poolAble == null)
            {
                throw new System.Exception("PoolAble object is null");
            }
            
            poolAble.Instance.SetActive(true);
            poolAble.OnDestroyed += ReturnToPool;
        
            return poolAble;
        }

        private void ReturnToPool(PoolAbleMonoBehaviour poolAble)
        {
            _freeObjects.Add(poolAble);
            poolAble.OnDestroyed -= ReturnToPool;
            poolAble.Instance.SetActive(false);
            poolAble.Instance.transform.SetParent(_container);
        }
    }
}