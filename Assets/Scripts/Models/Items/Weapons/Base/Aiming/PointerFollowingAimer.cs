using Core.Input.Interfaces;
using Models.Items.Weapons.Base.Aiming.Base;
using Models.Items.Weapons.Base.StatsHandling;
using UnityEngine;

namespace Models.Items.Weapons.Base.Aiming
{
    public class PointerFollowingAimer :Aimer
    {
        private readonly IInputHandler _inputHandler;
        
        public PointerFollowingAimer(
            WeaponStatsCalculator weaponStatsCalculator, 
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