using Core.Input.Interfaces;
using Models.Weapons.Services.Reloading.Interfaces;
using Models.Weapons.Services.Shooting.Enums;
using Models.Weapons.Services.Shooting.Interfaces;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Shooting.Factories
{
    public class WeaponShootersFactory
    {
        private readonly IInputHandler _inputHandler;
        
        public WeaponShootersFactory(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        public IShootService Create(
            ShootType shootType,
            IWeaponStatsCalculator statsCalculator,
            IMagazineService magazineService,
            Transform weaponTransform)
        {
            return shootType switch
            {
                ShootType.ByInput => new ByInputShooter(statsCalculator, magazineService, weaponTransform, _inputHandler),
                _ => throw new System.ArgumentOutOfRangeException(nameof(shootType), shootType, null)
            };
        }
    }
}