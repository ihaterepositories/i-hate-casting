using System;
using System.Collections;
using Core.RoundControl;
using Models.Items.Bullets.Abstraction;
using Models.Items.Weapons.Base.ScriptableObjects;
using PoolingCore;
using UnityEngine;
using Zenject;

namespace Models.Items.Weapons.Base
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] protected WeaponStatsSo _weaponStats;
        
        private ObjectPool<Bullet> _bulletsPool;
        private DiContainer _diContainer;
        
        private float _lastFireTime;
        private bool _isReloading;
        private int _bulletsInMagazine;
        
        public WeaponStatsSo WeaponStats => _weaponStats;
        public int BulletsInMagazine => _bulletsInMagazine;
        
        public event Action OnReloadNeeded;
        public event Action<float> OnReloadStarted;
        public event Action OnReloaded;
        
        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        private void Start()
        {
            _bulletsPool = new ObjectPool<Bullet>(_bulletPrefab.GetComponent<Bullet>(), _diContainer);
            _bulletsInMagazine = _weaponStats.GetMagazineCapacity();
            _lastFireTime = Time.time - _weaponStats.GetCooldownTime(); // Allow immediate fire on start
        }

        private void Update()
        {
            if (GameStateController.IsGamePaused()) return;
            
            RotateToTarget();
            
            if (GetFirePermission() && !_isReloading && _bulletsInMagazine > 0 && Time.time - _lastFireTime >= _weaponStats.GetCooldownTime())
            {
                Fire();
            }

            if (GetReloadPermission() && !_isReloading && _bulletsInMagazine < 1)
            {
                Reload();
            }
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
                
                bullet.Init(_weaponStats);
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
            OnReloadStarted?.Invoke(_weaponStats.GetReloadTime());
            StartCoroutine(ReloadCoroutine());
        }

        private IEnumerator ReloadCoroutine()
        {
            _isReloading = true;
            yield return new WaitForSeconds(_weaponStats.GetReloadTime());
            _bulletsInMagazine = _weaponStats.GetMagazineCapacity();
            _isReloading = false;
            OnReloaded?.Invoke();
        }

        protected abstract float GetFireDirectionAngle();
        
        protected abstract bool GetReloadPermission();
    }
}