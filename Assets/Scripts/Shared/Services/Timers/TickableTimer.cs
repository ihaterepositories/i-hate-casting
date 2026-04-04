using System;
using Shared.Services.Timers.Interfaces;
using UnityEngine;

namespace Shared.Services.Timers
{
    public class TickableTimer : ITimer
    {
        private float _timeLeft;
        
        public float TimeLeftToExpire => _timeLeft;
        public event Action OnTimerFinished;

        public TickableTimer(float duration)
        {
            _timeLeft = duration;
        }
        
        public void Reset(float duration, Action onTimerFinished)
        {
            OnTimerFinished = onTimerFinished;
            _timeLeft = duration;
        }
        
        public void Tick()
        {
            _timeLeft -= Time.deltaTime;

            if (_timeLeft <= 0)
            {
                OnTimerFinished?.Invoke();
                _timeLeft = 0;
            }
        }

        public void CleanResources()
        {
            OnTimerFinished = null;
        }
    }
}