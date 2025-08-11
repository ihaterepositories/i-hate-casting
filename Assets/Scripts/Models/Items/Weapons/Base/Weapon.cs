using System;
using System.Collections;
using Models.Items.Bullets.Abstraction;
using Models.Items.Weapons.Base.ScriptableObjects;
using PoolingCore;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Models.Items.Weapons.Base
{
    public abstract class Weapon : MonoBehaviour
    {
        [FormerlySerializedAs("bulletPrefab")] [SerializeField] private GameObject _bulletPrefab;
        [FormerlySerializedAs("weaponStats")] public WeaponStatsSo _weaponStats;
        
        private ObjectPool<Bullet> _bulletsPool;
        private float _lastFireTime;
        private DiContainer _diContainer;

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
            transform.rotation = Quaternion.Euler(0, 0, GetFireDirectionAngle());
            if (GetFirePermission() && !_isReloading && BulletsInMagazine > 0 && Time.time - _lastFireTime >= _weaponStats.GetCooldownTime())
            {
                _lastFireTime = Time.time;
                Fire();
                _bulletsInMagazine--;

                if (_bulletsInMagazine <= 0)
                {
                    OnReloadNeeded?.Invoke();
                }
            }

            if (GetReloadPermission() && !_isReloading)
            {
                OnReloadStarted?.Invoke(_weaponStats.GetReloadTime());
                Reload();
            }
        }

        protected abstract bool GetFirePermission();
        
        private void Fire()
        {
            if (_bulletsPool.GetFreeObject() is Bullet bullet)
            {
                bullet.Init(_weaponStats);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, GetFireDirectionAngle());
            }
            else
            {
                Debug.LogError($"Can`t create bullet in {gameObject.name}.");
            }
        }
        
        private void Reload()
        {
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