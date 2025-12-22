using System;
using System.Collections.Generic;
using Systems.Pooling.Models;
using UnityEngine;
using Zenject;

namespace Pooling
{
    public class ObjectPool<T> where T : PoolableMonoBehaviour
    {
        private readonly List<T> _freeObjects;
        private readonly Transform _container;
        private readonly GameObject _prefab;
        private readonly DiContainer _diContainer;
        
        public event Action<T> OnNewObjectCreated;

        public ObjectPool(GameObject prefab, DiContainer diContainer, Transform parentForObjects)
        {
            _freeObjects = new List<T>();
            _container = parentForObjects;
            _prefab = prefab;
            
            _diContainer = diContainer;
        }

        public T GetFreeObject()
        {
            T poolable;

            if (_freeObjects.Count > 0)
            {
                poolable = _freeObjects[0];
                _freeObjects.RemoveAt(0);
                
                if (poolable is null)
                    throw new Exception($"Pooled object is null: {_prefab.name}");
                
                poolable.Instance.SetActive(true);
                poolable.OnTakenFromPool();
            }
            else
            {
                poolable = _diContainer.InstantiatePrefab(_prefab, _container).GetComponent<T>();
                
                if (poolable is null)
                {
                    throw new Exception($"{_prefab.name} does not have a {typeof(T).Name} component!");
                }
                
                OnNewObjectCreated?.Invoke(poolable);
            }
            
            poolable.OnReturnToPool += ReturnToPool;
        
            return poolable;
        }

        private void ReturnToPool(PoolableMonoBehaviour poolable)
        {
            _freeObjects.Add(poolable as  T);
            poolable.OnReturnToPool -= ReturnToPool;
            poolable.Instance.SetActive(false);
            poolable.Instance.transform.SetParent(_container);
        }
    }
}