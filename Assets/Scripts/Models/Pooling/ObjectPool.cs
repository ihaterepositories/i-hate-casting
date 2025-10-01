using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Models.Pooling
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
                
                if (poolAble == null)
                    throw new Exception($"Pooled object is null: {_prefab.name}");
                
                poolAble.Instance.SetActive(true);
                poolAble.OnTakenFromPool();
            }
            else
            {
                _diContainer.InstantiatePrefab(_prefab, _container).TryGetComponent(out poolAble);

                if (poolAble == null)
                    throw new Exception($"Instantiated object does not implement PoolAble: {_prefab.name}");
            }
            
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