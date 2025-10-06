using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Models.WorldObjects.Base.Pooling
{
    public class ObjectPool<T> where T : PoolableMonoBehaviour
    {
        private readonly List<PoolableMonoBehaviour> _freeObjects;
        private readonly Transform _container;
        private readonly T _prefab;
        private readonly DiContainer _diContainer;

        public ObjectPool(T prefab, DiContainer diContainer, Transform parentForObjects)
        {
            _freeObjects = new List<PoolableMonoBehaviour>();
            _container = parentForObjects;
            _prefab = prefab;
            
            _diContainer = diContainer;
        }

        public PoolableMonoBehaviour GetFreeObject()
        {
            PoolableMonoBehaviour poolable;

            if (_freeObjects.Count > 0)
            {
                poolable = _freeObjects[0] as T;
                _freeObjects.RemoveAt(0);
                
                if (poolable == null)
                    throw new Exception($"Pooled object is null: {_prefab.name}");
                
                poolable.Instance.SetActive(true);
                poolable.OnTakenFromPool();
            }
            else
            {
                _diContainer.InstantiatePrefab(_prefab, _container).TryGetComponent(out poolable);

                if (poolable == null)
                    throw new Exception($"Instantiated object does not implement PoolAble: {_prefab.name}");
            }
            
            poolable.OnReturnedToPool += ReturnToPool;
        
            return poolable;
        }

        private void ReturnToPool(PoolableMonoBehaviour poolable)
        {
            _freeObjects.Add(poolable);
            poolable.OnReturnedToPool -= ReturnToPool;
            poolable.Instance.SetActive(false);
            poolable.Instance.transform.SetParent(_container);
        }
    }
}