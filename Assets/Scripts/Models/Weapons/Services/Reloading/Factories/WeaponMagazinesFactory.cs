using System;
using System.Collections.Generic;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Core.Input.Interfaces;
using Models.Bullets;
using Models.Bullets.Enums;
using Models.Weapons.Services.Reloading.Base;
using Models.Weapons.Services.Reloading.Enums;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using Spawners.Interfaces;

namespace Models.Weapons.Services.Reloading.Factories
{
    public class WeaponMagazinesFactory
    {
        private readonly IEventBusSubscriber _eventBusSubscriber;
        
        private readonly IInputHandler _inputHandler;
        private Dictionary<BulletType, ISpawner<Bullet>> _bulletSpawners;
        
        private bool _isInitialized;
        
        public WeaponMagazinesFactory(
            IEventBusSubscriber eventBusSubscriber,
            IInputHandler inputHandler)
        {
            _eventBusSubscriber = eventBusSubscriber;
            _inputHandler = inputHandler;
            
            _eventBusSubscriber.Subscribe<BulletSpawnersInitializedSignal>(Initialize);
        }
        
        public Magazine Create(
            BulletType bulletType,
            ReloadType reloadType,
            IWeaponStatsCalculator statsCalculator)
        {
            if (!_isInitialized) throw new Exception("Factory is not initialized!");
            
            return reloadType switch
            {
                ReloadType.ByInput => 
                    new ByInputControlledMagazine(_bulletSpawners[bulletType], statsCalculator, _inputHandler),
                
                _ => throw new ArgumentOutOfRangeException(nameof(reloadType), reloadType, null)
            };
        }
        
        private void Initialize(BulletSpawnersInitializedSignal bulletSpawnersInitializedSignal)
        { 
            _eventBusSubscriber.Unsubscribe<BulletSpawnersInitializedSignal>(Initialize);
            _bulletSpawners = bulletSpawnersInitializedSignal.BulletSpawners;
            _isInitialized = true;
        }
    }
}