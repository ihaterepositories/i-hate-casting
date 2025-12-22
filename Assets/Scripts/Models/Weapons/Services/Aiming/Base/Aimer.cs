using Models.Weapons.Services.Aiming.Interfaces;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Aiming.Base
{
    public abstract class Aimer : IAimService
    {
        private readonly IWeaponStatsCalculator _weaponStatsCalculator;
        private readonly Transform _weaponTransform;

        protected Aimer(IWeaponStatsCalculator weaponStatsCalculator, Transform weaponTransform)
        {
            _weaponStatsCalculator = weaponStatsCalculator;
            _weaponTransform = weaponTransform;
        }
        
        public void UpdateAiming()
        {
            Vector2 directionVector = (GetTargetPosition() - _weaponTransform.position);
            var rotationAngle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg + _weaponStatsCalculator.CalculateSpreadDegree();
            _weaponTransform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }

        protected abstract Vector3 GetTargetPosition();
    }
}