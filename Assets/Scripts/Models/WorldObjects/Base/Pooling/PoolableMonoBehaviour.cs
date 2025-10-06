using System;
using UnityEngine;

namespace Models.WorldObjects.Base.Pooling
{
    /// <summary>
    /// Base class for objects which spawning and dying during runtime.
    /// Use Destroy() method instead of classic Destroy(gameObject) to kill object.
    /// </summary>
    public class PoolableMonoBehaviour : MonoBehaviour
    {
        public GameObject Instance => gameObject;
        public event Action OnDestroyed;
        public event Action<PoolableMonoBehaviour> OnReturnedToPool;

        protected void ReturnToPool()
        {
            OnDestroyed?.Invoke();
            OnReturnedToPool?.Invoke(this);
        }
        
        /// <summary>
        /// This method is called every time when object is taken from pool.
        /// Use it like Awake() or Start() methods.
        /// </summary>
        public virtual void OnTakenFromPool()
        {
            
        }
    }
}