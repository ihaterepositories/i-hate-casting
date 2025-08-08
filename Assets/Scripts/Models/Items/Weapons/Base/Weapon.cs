using Models.Items.Bullets.Abstraction;
using Models.Items.Weapons.Base.ScriptableObjects;
using PoolingCore;
using UnityEngine;
using Zenject;

namespace Models.Items.Weapons.Base
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] protected WeaponStatsSO weaponStats;
        
        private ObjectPool<Bullet> _bulletsPool;
        private float _lastFireTime;
        private DiContainer _diContainer;
        
        protected bool IsReloading;
        protected int BulletsInMagazine;
        
        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        private void Start()
        {
            _bulletsPool = new ObjectPool<Bullet>(bulletPrefab.GetComponent<Bullet>(), _diContainer);
            BulletsInMagazine = weaponStats.GetMagazineCapacity();
            _lastFireTime = Time.time - weaponStats.GetCooldownTime(); // Allow immediate fire on start
        }

        private void Update()
        {
            transform.rotation = Quaternion.Euler(0, 0, GetFireDirectionAngle());
            if (GetFirePermission() && !IsReloading && BulletsInMagazine > 0 && Time.time - _lastFireTime >= weaponStats.GetCooldownTime())
            {
                _lastFireTime = Time.time;
                Fire();
                BulletsInMagazine--;
            }

            if (GetReloadPermission() && !IsReloading)
            {
                Reload();
            }
        }

        protected abstract bool GetFirePermission();
        
        private void Fire()
        {
            if (_bulletsPool.GetFreeObject() is Bullet bullet)
            {
                bullet.Init(weaponStats);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, GetFireDirectionAngle());
            }
            else
            {
                Debug.LogError($"Can`t create bullet in {gameObject.name}.");
            }
        }

        protected abstract float GetFireDirectionAngle();
        
        protected abstract bool GetReloadPermission();
        
        protected abstract void Reload();
    }
}