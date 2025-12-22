using Core.Input.Interfaces;
using Models.Weapons.Services.Reloading.Interfaces;
using Models.Weapons.Services.Shooting.Base;
using Models.Weapons.Services.Shooting.Interfaces;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Shooting
{
    public class ByInputShooter : Shooter, IShootService
    {
        private readonly IInputHandler _inputHandler;

        public ByInputShooter(
            IWeaponStatsCalculator statsCalculator, 
            IMagazineService magazineService, 
            Transform weaponTransform, 
            IInputHandler inputHandler) : base(statsCalculator, magazineService, weaponTransform)
        {
            _inputHandler = inputHandler;
        }

        protected override bool FireRule()
        {
            return _inputHandler.IsFireButtonPressed();
        }
    }
}