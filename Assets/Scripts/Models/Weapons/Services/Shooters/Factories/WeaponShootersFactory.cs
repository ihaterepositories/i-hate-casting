using Core.Input.Interfaces;
using Models.Weapons.Dtos;
using Models.Weapons.Services.Reloaders.Interfaces;
using Models.Weapons.Services.Shooters.Enums;
using Models.Weapons.Services.Shooters.Interfaces;
using Models.Weapons.Services.StatsScalers.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Shooters.Factories
{
    public class WeaponShootersFactory
    {
        private readonly IInputHandler _inputHandler;
        
        public WeaponShootersFactory(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        public IWeaponShooter Create(
            WeaponShootType weaponShootType,
            WeaponStats stats,
            IWeaponStatsScaler statsScaler,
            IWeaponReloader weaponReloader,
            Transform weaponTransform)
        {
            return weaponShootType switch
            {
                WeaponShootType.ByInput => new ByInputWeaponShooter(stats, statsScaler, weaponReloader, weaponTransform, _inputHandler),
                _ => throw new System.ArgumentOutOfRangeException(nameof(weaponShootType), weaponShootType, null)
            };
        }
    }
}