using System;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Core.Input.Interfaces;
using Models.Weapons.Services.Aiming.Enums;
using Models.Weapons.Services.Aiming.Interfaces;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Aiming.Factories
{
    public class WeaponAimersFactory
    {
        private readonly IEventBusSubscriber _eventBusSubscriber;
        
        private readonly IInputHandler _inputHandler;
        private Transform _playerTransform;
        
        private bool _isInitialized;
        
        public WeaponAimersFactory(
            IEventBusSubscriber eventBusSubscriber,
            IInputHandler inputHandler)
        {
            _eventBusSubscriber = eventBusSubscriber;
            _inputHandler = inputHandler;
            
            _eventBusSubscriber.Subscribe<PlayerSpawnedSignal>(Initialize);
        }
        
        public IAimService Create(AimType aimType, IWeaponStatsCalculator weaponStatsCalculator, Transform weaponTransform)
        {
            if (!_isInitialized) throw new Exception("Factory is not initialized!");
            
            return aimType switch
            {
                AimType.PointerFollowing => new PointerFollowingAimer(weaponStatsCalculator, weaponTransform, _inputHandler),
                AimType.PlayerFollowing => new PlayerFollowingAimer(weaponStatsCalculator, weaponTransform, _playerTransform),
                _ => null
            };
        }
        
        private void Initialize(PlayerSpawnedSignal playerSpawnedSignal)
        {
            _eventBusSubscriber.Unsubscribe<PlayerSpawnedSignal>(Initialize);
            _playerTransform = playerSpawnedSignal.Player.transform;
        }
    }
}