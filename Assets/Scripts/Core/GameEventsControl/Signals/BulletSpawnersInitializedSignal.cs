using System.Collections.Generic;
using Core.GameEventsControl.Signals.Interfaces;
using Models.Bullets;
using Models.Bullets.Enums;
using Spawners.Interfaces;

namespace Core.GameEventsControl.Signals
{
    public class BulletSpawnersInitializedSignal : IEventBusSignal
    {
        public BulletSpawnersInitializedSignal(Dictionary<BulletType, ISpawner<Bullet>> bulletSpawners)
        {
            BulletSpawners = bulletSpawners;
        }
        
        public Dictionary<BulletType, ISpawner<Bullet>> BulletSpawners { get; private set; }
    }
}