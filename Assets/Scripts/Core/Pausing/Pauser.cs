using System;
using System.Threading.Tasks;
using Core.Pausing.Constants;
using Core.Pausing.Interfaces;
using UnityEngine;

namespace Core.Pausing
{
    public class Pauser : IPauser
    {
        private bool _isPaused;

        public bool IsGamePaused => _isPaused;
        
        public event Action OnPaused;
        public event Action OnResumeStarted;
        public event Action OnResumeCrashed;

        public void Pause()
        {
            Time.timeScale = 0;
            _isPaused = true;
            OnPaused?.Invoke();
        }

        public void Resume()
        {
            if (!_isPaused) return;
            _ = ResumeAsync();
        }
        
        private async Task ResumeAsync()
        {
            OnResumeStarted?.Invoke();
            
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(PausingConstants.RESUME_TIME));
            }
            catch (Exception)
            {
                OnResumeCrashed?.Invoke();
            }
            finally
            {
                Time.timeScale = 1;
                _isPaused = false;
            }
        }
    }
}