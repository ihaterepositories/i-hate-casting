using Core.Input.Interfaces;
using Models.Weapons.Services.Aiming.Base;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Aiming
{
    public class PointerFollowingAimer :Aimer
    {
        private readonly IInputHandler _inputHandler;
        
        public PointerFollowingAimer(
            IWeaponStatsCalculator weaponStatsCalculator, 
            Transform weaponTransform,
            IInputHandler inputHandler) : base(weaponStatsCalculator, weaponTransform)
        {
            _inputHandler = inputHandler;
        }

        protected override Vector3 GetTargetPosition()
        {
            return _inputHandler.GetPointerPosition();
        }
    }
}