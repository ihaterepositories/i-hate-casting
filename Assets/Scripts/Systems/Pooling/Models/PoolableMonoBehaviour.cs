using System;
using UnityEngine;

namespace Systems.Pooling.Models
{
    /// <summary>
    /// Base class for poolable objects.
    /// Use ReturnToPool() method instead of classic Destroy(gameObject) to destroy object.
    /// </summary>
    public class PoolableMonoBehaviour : MonoBehaviour
    {
        public GameObject Instance => gameObject;
        public event Action<PoolableMonoBehaviour> OnReturnToPool;
        
        /// <summary>
        /// This method is called every time when object is taken from pool.
        /// Use it like Awake() or Start() methods.
        /// </summary>
        public virtual void OnTakenFromPool() { }
        
        /// <summary>
        /// Returns object to it`s object pool.
        /// </summary>
        public void ReturnToPool()
        {
            OnReturnToPool?.Invoke(this);
        }
    }
}