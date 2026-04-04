using Core.Input.Interfaces;
using Models.Weapons.Dtos;
using Models.Weapons.Services.Aimers.Base;
using Models.Weapons.Services.StatsScalers.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Aimers
{
    public class PointerFollowingWeaponAimer :WeaponAimer
    {
        private readonly IInputHandler _inputHandler;
        
        public PointerFollowingWeaponAimer(
            WeaponStats weaponStats,
            IWeaponStatsScaler statsScaler, 
            Transform weaponTransform,
            IInputHandler inputHandler) 
            : base(weaponStats, statsScaler, weaponTransform)
        {
            _inputHandler = inputHandler;
        }

        protected override Vector3 GetTargetPosition()
        {
            return _inputHandler.GetPointerPosition();
        }
    }
}