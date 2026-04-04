using Shared.Services.Timers.Interfaces;

namespace Core.TimerEngines.Dtos
{
    public class TimerData
    {
        public TimerData(ITimer timer)
        {
            Timer = timer;
            IsRunning = false;
        }
        
        public ITimer Timer { get; }

        public bool IsRunning { get; set; }
    }
}