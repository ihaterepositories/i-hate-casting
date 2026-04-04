using System;
using Shared.Systems.ResourcesCleaning.Interfaces;
using UnityEngine;

namespace Shared.Services.Timers.Interfaces
{
    public interface ITimer : IResourceCleanable
    {
        public float TimeLeftToExpire { get; }
        
        /// <summary>
        /// Invokes once the time is left.
        /// </summary>
        public event Action OnTimerFinished;
        
        /// <summary>
        /// Represents one update step. Internally decreases remaining time using <see cref="Time.deltaTime"/>.
        /// Invokes OnTimerFinished once the time is left.
        /// </summary>
        /// <remarks>
        /// This method should be called once per frame (e.g. in Update() loop) while the timer is active.
        /// </remarks>
        public void Tick();
        
        /// <summary>
        /// Sets new duration and cleans all callbacks.
        /// </summary>
        /// <param name="duration">New duration,</param>
        /// <param name="onTimerFinished">Callback that will be invoked once the timer completes.</param>
        public void Reset(float duration, Action onTimerFinished);
    }
}