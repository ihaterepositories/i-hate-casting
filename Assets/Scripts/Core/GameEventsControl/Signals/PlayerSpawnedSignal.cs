using Core.GameEventsControl.Signals.Interfaces;
using Models.Creatures;

namespace Core.GameEventsControl.Signals
{
    public class PlayerSpawnedSignal : IEventBusSignal
    {
        public PlayerSpawnedSignal(Creature player)
        {
            Player = player;
        }
        
        public Creature Player { get; }
    }
}