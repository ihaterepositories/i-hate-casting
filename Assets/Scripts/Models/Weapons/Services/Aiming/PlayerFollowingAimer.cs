using Models.Weapons.Services.Aiming.Base;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Aiming
{
    public class PlayerFollowingAimer : Aimer
    {
        private readonly Transform _playerTransform;
        
        public PlayerFollowingAimer(
            IWeaponStatsCalculator weaponStatsCalculator, 
            Transform weaponTransform,
            Transform playerTransform) : base(weaponStatsCalculator, weaponTransform)
        {
            _playerTransform = playerTransform;
        }

        protected override Vector3 GetTargetPosition()
        {
            return _playerTransform.position;
        }
    }
}