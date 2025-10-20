using Core.Input.Interfaces;
using Models.Items.Weapons.Base.Reloading.Interfaces;
using Models.Items.Weapons.Base.Shooting.Base;
using Models.Items.Weapons.Base.StatsHandling;
using UnityEngine;

namespace Models.Items.Weapons.Base.Shooting
{
    public class ByInputShooter : Shooter
    {
        private readonly IInputHandler _inputHandler;

        public ByInputShooter(
            WeaponStatsCalculator statsCalculator, 
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