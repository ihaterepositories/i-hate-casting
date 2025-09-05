using System;
using UnityEngine;

namespace Models.Pooling
{
    /// <summary>
    /// Base class for objects which spawning and dying during runtime.
    /// Use Destroy() method instead of classic Destroy(gameObject) to kill object.
    /// </summary>
    public class PoolAbleMonoBehaviour : MonoBehaviour
    {
        public GameObject Instance => gameObject;
        public event Action<PoolAbleMonoBehaviour> OnDestroyed;

        protected void ReturnToPool()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}