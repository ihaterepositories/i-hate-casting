using System;
using UnityEngine;

namespace Pooling.Interfaces
{
    public interface IPoolAble
    {
        public GameObject GameObject { get; }
        public event Action<IPoolAble> OnDestroyed;
        public void Reset();
    }
}