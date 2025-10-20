using Models.Items.Bullets.Base.Enums;
using Models.Items.Weapons.Base.Reloading.Interfaces;
using Models.Items.Weapons.Base.Shooting.Interfaces;
using Models.Items.Weapons.Base.StatsHandling;
using UnityEngine;

namespace Models.Items.Weapons.Base.Shooting.Base
{
    public abstract class Shooter : IShootService
    {
        private readonly WeaponStatsCalculator _statsCalculator;
        private readonly IMagazineService _magazineService;
        private readonly Transform _weaponTransform;

        private BulletType _bulletType;
        private float _lastFireTime;
        
        protected Shooter(
            WeaponStatsCalculator statsCalculator,
            IMagazineService magazineService,
            Transform weaponTransform)
        {
            _statsCalculator = statsCalculator;
            _magazineService = magazineService;
            _weaponTransform = weaponTransform;
            
            _lastFireTime = Time.time - _statsCalculator.GetCooldownTime(); // Allow immediate fire on start
        }

        public float CooldownDuration => _statsCalculator.GetCooldownTime();
        public float CooldownTimeElapsed => Time.time - _lastFireTime;

        public void EnableShoot()
        {
            if (_bulletType == BulletType.None)
            {
                Debug.LogWarning("Assign bullet type to the shooter before shooting.");
                return;
            }
            
            if (!FireRule()) return;
            
            if (_magazineService.IsMagazineEmpty) return;
            if (Time.time - _lastFireTime < _statsCalculator.GetCooldownTime()) return;
            
            var bullet = _magazineService.GetBullet(_bulletType);
            bullet.transform.position = _weaponTransform.position;
            
            float spreadAngle = _statsCalculator.GetSpreadDegree();
            float baseAngle = _weaponTransform.eulerAngles.z;

            bullet.transform.rotation = Quaternion.Euler(0, 0, baseAngle + spreadAngle);
            
            _lastFireTime = Time.time;
        }

        public void ChangeBulletsType(BulletType bulletType)
        {
            _bulletType = bulletType;
        }
        
        protected abstract bool FireRule();
    }
}