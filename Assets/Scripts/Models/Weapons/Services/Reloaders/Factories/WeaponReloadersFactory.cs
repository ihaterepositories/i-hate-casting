using System;
using System.Collections.Generic;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Core.Input.Interfaces;
using Core.TimerEngines.Interfaces;
using Models.Bullets;
using Models.Bullets.Enums;
using Models.Weapons.Dtos;
using Models.Weapons.Services.Reloaders.Base;
using Models.Weapons.Services.Reloaders.Enums;
using Models.Weapons.Services.StatsScalers.Interfaces;
using Spawners.Interfaces;

namespace Models.Weapons.Services.Reloaders.Factories
{
    public class WeaponReloadersFactory
    {
        private readonly IEventBusSubscriber _eventBusSubscriber;
        private readonly IInputHandler _inputHandler;
        private readonly ITimerEngine _timerEngine;
        
        private Dictionary<BulletType, ISpawner<Bullet>> _bulletSpawners;
        
        private bool _isInitialized;
        
        public WeaponReloadersFactory(
            IEventBusSubscriber eventBusSubscriber,
            IInputHandler inputHandler,
            ITimerEngine timerEngine)
        {
            _eventBusSubscriber = eventBusSubscriber;
            _inputHandler = inputHandler;
            _timerEngine = timerEngine;
            
            _eventBusSubscriber.Subscribe<BulletSpawnersInitializedSignal>(Initialize);
        }
        
        public WeaponReloader Create(
            BulletType bulletType,
            WeaponReloadType weaponReloadType,
            WeaponStats stats,
            IWeaponStatsScaler statsScaler)
        {
            if (!_isInitialized) throw new Exception("Factory is not initialized!");
            
            return weaponReloadType switch
            {
                WeaponReloadType.ByInput => 
                    new ByInputWeaponReloader(stats, statsScaler, _bulletSpawners[bulletType], _inputHandler, _timerEngine),
                
                _ => throw new ArgumentOutOfRangeException(nameof(weaponReloadType), weaponReloadType, null)
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