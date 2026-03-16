using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Pausing.Interfaces;
using UnityEngine;
using Utils.Timers.Interfaces;

namespace Utils.Timers
{
    public class AsyncTimer : ITimer
    {
        private readonly IPauser _pauser;
        private CancellationTokenSource _cts;

        public AsyncTimer(IPauser pauser)
        {
            _pauser = pauser;
        }
        
        public float TimeLeftToExpire { get; private set; }
        public event Action OnTimeExpired;

        public void Start(float time)
        {
            _ = StartAsync(time);
        }

        private async Task StartAsync(float time)
        {
            var timeLeft = time;
            _cts = new CancellationTokenSource();

            try
            {
                while (timeLeft > 0)
                {
                    _cts.Token.ThrowIfCancellationRequested();

                    if (!_pauser.IsGamePaused)
                    {
                        timeLeft -= Time.deltaTime;
                        TimeLeftToExpire = timeLeft;
                    }

                    await Task.Yield(); // Wait until next frame
                }
                
                OnTimeExpired?.Invoke();
                TimeLeftToExpire = 0f;
            }
            catch (OperationCanceledException)
            {
                // Timer stopped
            }
        }

        public void Stop()
        {
            if (_cts == null) return;

            if (!_cts.IsCancellationRequested)
                _cts.Cancel();
            
            _cts.Dispose();
            _cts = null;
        }
    }
}