using System;

namespace Core.Pausing.Interfaces
{
    public interface IPauser
    {
        public bool IsGamePaused { get; }
        
        public event Action OnPaused; 
        public event Action OnResumeStarted;
        public event Action OnResumeCrashed;
        
        public void Pause();
        public void Resume();
    }
}