using System;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Core.Input.Interfaces;
using Models.Weapons.Dtos;
using Models.Weapons.Services.Aimers.Enums;
using Models.Weapons.Services.Aimers.Interfaces;
using Models.Weapons.Services.StatsScalers.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Aimers.Factories
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
        
        public IWeaponAimer Create(
            WeaponAimType weaponAimType, 
            WeaponStats stats,
            IWeaponStatsScaler statsScaler, 
            Transform weaponTransform)
        {
            if (!_isInitialized) throw new Exception("Factory is not initialized!");
            
            return weaponAimType switch
            {
                WeaponAimType.PointerFollowing => new PointerFollowingWeaponAimer(stats, statsScaler, weaponTransform, _inputHandler),
                WeaponAimType.PlayerFollowing => new PlayerFollowingWeaponAimer(stats, statsScaler, weaponTransform, _playerTransform),
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