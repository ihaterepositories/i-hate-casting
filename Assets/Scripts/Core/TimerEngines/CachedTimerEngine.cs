using System;
using System.Collections.Generic;
using Core.Pausing.Interfaces;
using Core.TimerEngines.Dtos;
using Core.TimerEngines.Interfaces;
using Shared.Services.Timers;
using UnityEngine;
using Zenject;

namespace Core.TimerEngines
{
    public class CachedTimerEngine : MonoBehaviour, ITimerEngine
    {
        private List<TimerData> _timers = new();

        private IPauser _pauser;

        [Inject]
        private void Construct(IPauser pauser)
        {
            _pauser = pauser;
        }
        
        private void Update()
        {
            if (_pauser.IsGamePaused) return;
            
            foreach (var timerData in _timers)
            {
                if (timerData.IsRunning)
                {
                    timerData.Timer.Tick();
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var timerData in _timers)
            {
                timerData.Timer?.CleanResources();
            }
            
            _timers.Clear();
        }

        public void StartTimer(float duration, Action onTimerFinished)
        {
            foreach (var timerData in _timers)
            {
                if (!timerData.IsRunning)
                {
                    timerData.Timer.Reset(duration, onTimerFinished);
                    timerData.IsRunning = true;
                    return;
                }
            }
            
            // if free timer wasn't found, then:
            
            var newTimerData = new TimerData(new TickableTimer(duration));
            
            newTimerData.Timer.OnTimerFinished += () =>
            {
                newTimerData.IsRunning = false;
                onTimerFinished?.Invoke();
            };
            
            _timers.Add(newTimerData);
        }
    }
}