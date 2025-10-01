using System;
using System.Collections;
using Core.GameControl;
using Models.Items.Weapons.Base.Enums;
using Models.Items.Weapons.Base.StatsHandling;
using Models.Items.Weapons.Base.StatsHandling.DataContainers;
using Models.Items.Weapons.Bullets.Base;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Models.Items.Weapons.Base
{
    /// <summary>
    /// Child class must assign the specific bullets pool in the Construct (Inject) method.
    /// </summary>
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponStats _weaponStats;

        private WeaponStatsMultipliersProvider _weaponStatsMultipliersProvider;
        protected WeaponStatsCalculator _weaponStatsCalculator;
        
        private float _lastFireTime;
        private bool _isReloading;
        private int _bulletsInMagazine;
        
        public int BulletsInMagazine => _bulletsInMagazine;
        public WeaponStatsCalculator WeaponStatsCalculator => _weaponStatsCalculator;
        public WeaponType WeaponType => _weaponStats.WeaponType;
        
        public event Action OnReloadNeeded;
        public event Action<float> OnReloadStarted;
        public event Action OnReloaded;
        
        [Inject]
        private void Construct(WeaponStatsMultipliersProvider weaponStatsMultipliersProvider)
        {
            _weaponStatsMultipliersProvider = weaponStatsMultipliersProvider;
        }

        private void Awake()
        {
            var weaponStatsMultiplier = _weaponStatsMultipliersProvider.GetFor(_weaponStats.WeaponType);
            _weaponStatsCalculator = new WeaponStatsCalculator(_weaponStats, weaponStatsMultiplier);
        }

        private void Start()
        {
            _bulletsInMagazine = _weaponStatsCalculator.GetMagazineCapacity();
            _lastFireTime = Time.time - _weaponStatsCalculator.GetCooldownTime(); // Allow immediate fire on start
        }

        private void Update()
        {
            if (GamePauser.IsGamePaused) return;
            
            RotateToTarget();
            
            if (GetFirePermission() && !_isReloading && _bulletsInMagazine > 0 && Time.time - _lastFireTime >= _weaponStatsCalculator.GetCooldownTime())
            {
                Fire();
            }

            if (GetReloadPermission() && !_isReloading && _bulletsInMagazine < 1)
            {
                Reload();
            }
        }

        protected float CalculateSpread()
        {
            return Random.Range(-_weaponStatsCalculator.GetSpread(), _weaponStatsCalculator.GetSpread());
        }
        
        private void RotateToTarget()
        {
            transform.rotation = Quaternion.Euler(0, 0, GetFireDirectionAngle());
        }

        protected abstract bool GetFirePermission();
        
        private void Fire()
        {
            var bullet = GetBulletFromPool();
            _lastFireTime = Time.time;
            
            bullet.Init(_weaponStatsCalculator);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, GetFireDirectionAngle());
            
            _bulletsInMagazine--;
            
            if (_bulletsInMagazine <= 0)
            {
                OnReloadNeeded?.Invoke();
            }
        }
        
        protected abstract Bullet GetBulletFromPool();
        
        private void Reload()
        {
            OnReloadStarted?.Invoke(_weaponStatsCalculator.GetReloadTime());
            StartCoroutine(ReloadCoroutine());
        }

        private IEnumerator ReloadCoroutine()
        {
            _isReloading = true;
            yield return new WaitForSeconds(_weaponStatsCalculator.GetReloadTime());
            _bulletsInMagazine = _weaponStatsCalculator.GetMagazineCapacity();
            _isReloading = false;
            OnReloaded?.Invoke();
        }

        protected abstract float GetFireDirectionAngle();
        
        protected abstract bool GetReloadPermission();
    }
}