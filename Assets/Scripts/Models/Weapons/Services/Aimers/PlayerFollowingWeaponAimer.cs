using Models.Weapons.Dtos;
using Models.Weapons.Services.Aimers.Base;
using Models.Weapons.Services.StatsScalers.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Aimers
{
    public class PlayerFollowingWeaponAimer : WeaponAimer
    {
        private readonly Transform _playerTransform;
        
        public PlayerFollowingWeaponAimer(
            WeaponStats weaponStats,
            IWeaponStatsScaler statsScaler, 
            Transform weaponTransform,
            Transform playerTransform) 
            : base(weaponStats, statsScaler, weaponTransform)
        {
            _playerTransform = playerTransform;
        }

        protected override Vector3 GetTargetPosition()
        {
            return _playerTransform.position;
        }
    }
}