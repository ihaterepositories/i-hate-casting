using System;
using System.Collections;
using Core.GameControl;
using Models.Items.Bullets.Base;
using Models.Items.Weapons.Base.StatsHandling;
using Models.Items.Weapons.Base.StatsHandling.ScriptableObjects;
using Pooling;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Models.Items.Weapons.Base
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] protected WeaponStatsSo _weaponStats;
        
        private ObjectPool<Bullet> _bulletsPool;
        private DiContainer _diContainer;

        private WeaponStatsMultipliersProvider _weaponStatsMultipliersProvider;
        protected WeaponStatsCalculator _weaponStatsCalculator;
        
        private float _lastFireTime;
        private bool _isReloading;
        private int _bulletsInMagazine;
        
        public int BulletsInMagazine => _bulletsInMagazine;
        public WeaponStatsCalculator WeaponStatsCalculator => _weaponStatsCalculator;
        
        public event Action OnReloadNeeded;
        public event Action<float> OnReloadStarted;
        public event Action OnReloaded;
        
        [Inject]
        private void Construct(DiContainer diContainer, WeaponStatsMultipliersProvider weaponStatsMultipliersProvider)
        {
            _diContainer = diContainer;
            _weaponStatsMultipliersProvider = weaponStatsMultipliersProvider;
        }

        private void Awake()
        {
            var weaponStatsMultiplier = _weaponStatsMultipliersProvider.GetFor(_weaponStats.WeaponType);
            _weaponStatsCalculator = new WeaponStatsCalculator(_weaponStats, weaponStatsMultiplier);
            _bulletsPool = new ObjectPool<Bullet>(_bulletPrefab.GetComponent<Bullet>(), _diContainer);
        }

        private void Start()
        {
            _bulletsInMagazine = _weaponStatsCalculator.GetMagazineCapacity();
            _lastFireTime = Time.time - _weaponStatsCalculator.GetCooldownTime(); // Allow immediate fire on start
        }

        private void Update()
        {
            if (GameStateHolder.IsGamePaused) return;
            
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
            if (_bulletsPool.GetFreeObject() is Bullet bullet)
            {
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
            else
            {
                Debug.LogError($"Can`t create bullet in {gameObject.name}.");
            }
        }
        
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