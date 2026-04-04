using System;

namespace Core.TimerEngines.Interfaces
{
    public interface ITimerEngine
    {
        /// <summary>
        /// Starts a new timer that will invoke a callback after the specified duration.
        /// </summary>
        /// <param name="duration">
        /// Duration of the timer in seconds.
        /// Must be greater than or equal to zero.
        /// </param>
        /// <param name="onTimerFinished">
        /// Callback that will be invoked once the timer completes.
        /// Callback is self cleanable! You don't have to unsubscribe it.
        /// </param>
        /// <remarks>
        /// Implementation may reuse previously created timer instances
        /// for performance optimization.
        /// </remarks>
        public void StartTimer(float duration, Action onTimerFinished);
    }
}