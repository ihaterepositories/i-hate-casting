using Models.Items.Weapons.Base.Aiming.Interfaces;
using Models.Items.Weapons.Base.StatsHandling;
using UnityEngine;

namespace Models.Items.Weapons.Base.Aiming.Base
{
    public abstract class Aimer : IAimService
    {
        private readonly WeaponStatsCalculator _weaponStatsCalculator;
        private readonly Transform _weaponTransform;

        protected Aimer(WeaponStatsCalculator weaponStatsCalculator, Transform weaponTransform)
        {
            _weaponStatsCalculator = weaponStatsCalculator;
            _weaponTransform = weaponTransform;
        }
        
        public void UpdateAiming()
        {
            Vector2 directionVector = (GetTargetPosition() - _weaponTransform.position);
            var rotationAngle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg + _weaponStatsCalculator.GetSpreadDegree();
            _weaponTransform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }

        protected abstract Vector3 GetTargetPosition();
    }
}