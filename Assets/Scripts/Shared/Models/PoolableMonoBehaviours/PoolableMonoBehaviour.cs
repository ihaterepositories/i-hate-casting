using System;
using UnityEngine;

namespace Shared.Models.PoolableMonoBehaviours
{
    /// <summary>
    /// Base class for poolable objects.
    /// Use ReturnToPool() method instead of classic Destroy(gameObject) to destroy object.
    /// </summary>
    public class PoolableMonoBehaviour : MonoBehaviour
    {
        public GameObject Instance => gameObject;
        public event Action<PoolableMonoBehaviour> OnReturnToPoolCalled;
        
        /// <summary>
        /// This method is called every time when this object is taken from pool.
        /// </summary>
        public virtual void OnTakenFromPool() { }
        
        /// <summary>
        /// Returns object to it`s object pool.
        /// </summary>
        public void ReturnToPool()
        {
            // If game object was created throw ObjectPool, OnReturnToPoolCalled can't be null.
            // ObjectPool subscribes it's returning to pool logic on this event, during the instantiating process.
            if (OnReturnToPoolCalled == null)
            {
                throw new Exception("You are trying to return object to its object pool." +
                                    "But it seems like this object is not registered in the object pool.");
            }
            
            OnReturnToPoolCalled?.Invoke(this);
        }
    }
}