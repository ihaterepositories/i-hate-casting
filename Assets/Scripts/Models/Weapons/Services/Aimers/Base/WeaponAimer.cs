using Models.Weapons.Dtos;
using Models.Weapons.Services.Aimers.Interfaces;
using Models.Weapons.Services.StatsScalers.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Aimers.Base
{
    public abstract class WeaponAimer : IWeaponAimer
    {
        private readonly WeaponStats _stats;
        private readonly IWeaponStatsScaler _statsScaler;
        private readonly Transform _weaponTransform;

        protected WeaponAimer(
            WeaponStats stats,
            IWeaponStatsScaler statsScaler, 
            Transform weaponTransform)
        {
            _stats = stats;
            _statsScaler = statsScaler;
            _weaponTransform = weaponTransform;
        }
        
        public Quaternion Tick()
        {
            Vector2 directionVector = (GetTargetPosition() - _weaponTransform.position);
            
            var rotationAngle = 
                Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg // rotation degrees to aim the target
                + _statsScaler.ScaleSpreadDegree(_stats.SpreadDegree); // adding weapon`s spread
            
            return Quaternion.Euler(0, 0, rotationAngle);
        }

        protected abstract Vector3 GetTargetPosition();
    }
}