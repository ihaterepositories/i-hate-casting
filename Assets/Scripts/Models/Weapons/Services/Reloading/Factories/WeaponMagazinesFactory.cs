using System;
using System.Collections.Generic;
using Core;
using Core.Input.Interfaces;
using Models.Bullets;
using Models.Bullets.Dtos;
using Models.Bullets.Enums;
using Models.Weapons.Services.Reloading.Base;
using Models.Weapons.Services.Reloading.Enums;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using Spawners.Interfaces;

namespace Models.Weapons.Services.Reloading.Factories
{
    public class WeaponMagazinesFactory
    {
        private readonly IInputHandler _inputHandler;
        private readonly SpawnersInitializer _spawnersInitializer;
        
        private bool _isSpawnersInitialized;
        
        public WeaponMagazinesFactory(
            IInputHandler inputHandler,
            SpawnersInitializer spawnersInitializer)
        {
            _inputHandler = inputHandler;
            _spawnersInitializer = spawnersInitializer;
            
            _spawnersInitializer.OnInitialized += SetInitializeFlag;
        }
        
        public Magazine Create(
            BulletType bulletType,
            ReloadType reloadType,
            IWeaponStatsCalculator statsCalculator)
        {
            if (!_isSpawnersInitialized) throw new Exception("Bullet spawners not initialized!");
            
            return reloadType switch
            {
                ReloadType.ByInput => 
                    new ByInputControlledMagazine(_spawnersInitializer.BulletSpawners[bulletType], statsCalculator, _inputHandler),
                
                _ => throw new ArgumentOutOfRangeException(nameof(reloadType), reloadType, null)
            };
        }
        
        private void SetInitializeFlag()
        { 
            _isSpawnersInitialized = true;
            _spawnersInitializer.OnInitialized -= SetInitializeFlag;
        }
    }
}