using System;
using UnityEngine;

namespace PoolingCore.Interfaces
{
    public interface IPoolAble
    {
        public GameObject GameObject { get; }
        public event Action<IPoolAble> OnDestroyed;
        public void Reset();
    }
}