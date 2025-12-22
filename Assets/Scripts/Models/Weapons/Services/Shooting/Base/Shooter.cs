using Models.Bullets.Dtos;
using Models.Creatures.Enums;
using Models.Weapons.Services.Reloading.Interfaces;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Shooting.Base
{
    public abstract class Shooter
    {
        private readonly CreatureType _owner;
        private readonly IWeaponStatsCalculator _statsCalculator;
        private readonly IMagazineService _magazineService;
        private readonly Transform _weaponTransform;
        
        private float _lastFireTime;
        
        protected Shooter(
            IWeaponStatsCalculator statsCalculator,
            IMagazineService magazineService,
            Transform weaponTransform)
        {
            _statsCalculator = statsCalculator;
            _magazineService = magazineService;
            _weaponTransform = weaponTransform;
            
            _lastFireTime = Time.time - _statsCalculator.CalculateCooldownTime(); // Allow immediate fire on start
        }

        public float CooldownDuration => _statsCalculator.CalculateCooldownTime();
        public float CooldownTimeElapsed => Time.time - _lastFireTime;

        public void EnableShoot()
        {
            if (!FireRule()) return;
            
            if (_magazineService.IsMagazineEmpty) return;
            if (Time.time - _lastFireTime < _statsCalculator.CalculateCooldownTime()) return;
            
            Shoot();
        }

        private void Shoot()
        {
            var bullet = _magazineService.GetBullet();
            
            var rotationAngle = _weaponTransform.eulerAngles.z;
            var spreadAngle = _statsCalculator.CalculateSpreadDegree();
            
            bullet.Launch(new BulletLaunchData(
                _statsCalculator.CalculateSpeed(),
                _statsCalculator.CalculateRange(),
                _statsCalculator.CalculateDamageToDeal(), 
                _weaponTransform.position, 
                rotationAngle,
                spreadAngle));
            
            _lastFireTime = Time.time;
        }
        
        protected abstract bool FireRule();
    }
}