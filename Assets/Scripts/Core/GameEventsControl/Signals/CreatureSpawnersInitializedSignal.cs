using System.Collections.Generic;
using Core.GameEventsControl.Signals.Interfaces;
using Models.Creatures;
using Models.Creatures.Enums;
using Spawners.Interfaces;

namespace Core.GameEventsControl.Signals
{
    public class CreatureSpawnersInitializedSignal : IEventBusSignal
    {
        public CreatureSpawnersInitializedSignal(Dictionary<CreatureType, ISpawner<Creature>> creatureSpawners)
        {
            CreaturesSpawners = creatureSpawners;
        }
        
        public Dictionary<CreatureType, ISpawner<Creature>> CreaturesSpawners {get; private set;}
    }
}