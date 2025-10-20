using Core.Input.Interfaces;
using Models.Items.Weapons.Base.Reloading.Interfaces;
using Models.Items.Weapons.Base.Shooting.Base;
using Models.Items.Weapons.Base.Shooting.Enums;
using Models.Items.Weapons.Base.StatsHandling;
using UnityEngine;

namespace Models.Items.Weapons.Base.Shooting.Factories
{
    public class ShootersFactory
    {
        private readonly IInputHandler _inputHandler;
        
        public ShootersFactory(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        public Shooter Create(
            ShootType shootType,
            WeaponStatsCalculator statsCalculator,
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