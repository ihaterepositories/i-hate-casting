using Core.GameEventsControl.Signals.Interfaces;
using Utils.Timers.Interfaces;

namespace Core.GameEventsControl.Signals
{
    public class RoundTimerStarted : IEventBusSignal
    {
        public RoundTimerStarted(ITimer roundTimer)
        {
            RoundTimer = roundTimer;
        }
        
        public ITimer RoundTimer { get; private set; }
    }
}