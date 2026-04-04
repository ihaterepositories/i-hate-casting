using Models.Bullets.Dtos;
using Models.Creatures.Enums;
using Models.Weapons.Dtos;
using Models.Weapons.Services.Reloaders.Interfaces;
using Models.Weapons.Services.Shooters.Interfaces;
using Models.Weapons.Services.StatsScalers.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Shooters.Base
{
    public abstract class WeaponShooter : IWeaponShooter
    {
        private readonly CreatureType _owner;
        private readonly WeaponStats _stats;
        private readonly IWeaponStatsScaler _statsScaler;
        private readonly IWeaponReloader _weaponReloader;
        private readonly Transform _weaponTransform;
        
        private float _lastFireTime;
        
        protected WeaponShooter(
            WeaponStats stats,
            IWeaponStatsScaler statsScaler,
            IWeaponReloader weaponReloader,
            Transform weaponTransform)
        {
            _stats = stats;
            _statsScaler = statsScaler;
            _weaponReloader = weaponReloader;
            _weaponTransform = weaponTransform;
            
            // Resetting _lastFireTime to allow fire on the start without delay
            _lastFireTime = Time.time - _statsScaler.ScaleCooldownTime(_stats.CooldownTime);
        }

        public float CooldownTime => _statsScaler.ScaleCooldownTime(_stats.CooldownTime);
        public float CooldownTimeElapsed => Time.time - _lastFireTime;

        public void Tick()
        {
            if (!FireRule()) return;
            
            if (_weaponReloader.IsMagazineEmpty) return;
            if (Time.time - _lastFireTime < _statsScaler.ScaleCooldownTime(_stats.CooldownTime)) return;
            
            Shoot();
        }

        private void Shoot()
        {
            var bullet = _weaponReloader.GetBullet();
            
            var rotationAngle = _weaponTransform.eulerAngles.z;
            var spreadAngle = _statsScaler.ScaleSpreadDegree(_stats.SpreadDegree);
            
            bullet.Launch(new BulletLaunchData(
                _statsScaler.ScaleSpeed(_stats.Speed),
                _stats.Range,
                _statsScaler.ScaleDamageToDeal(_stats.DamageToDeal), 
                _weaponTransform.position, 
                rotationAngle,
                spreadAngle));
            
            _lastFireTime = Time.time;
        }
        
        protected abstract bool FireRule();
    }
}