using Core.Input.Interfaces;
using Models.Weapons.Dtos;
using Models.Weapons.Services.Reloaders.Interfaces;
using Models.Weapons.Services.Shooters.Base;
using Models.Weapons.Services.StatsScalers.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Shooters
{
    public class ByInputWeaponShooter : WeaponShooter
    {
        private readonly IInputHandler _inputHandler;

        public ByInputWeaponShooter(
            WeaponStats stats,
            IWeaponStatsScaler statsScaler, 
            IWeaponReloader weaponReloader, 
            Transform weaponTransform, 
            IInputHandler inputHandler) : base(stats, statsScaler, weaponReloader, weaponTransform)
        {
            _inputHandler = inputHandler;
        }

        protected override bool FireRule()
        {
            return _inputHandler.IsFireButtonPressed();
        }
    }
}