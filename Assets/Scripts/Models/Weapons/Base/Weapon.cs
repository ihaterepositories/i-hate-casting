using Models.Bullets.Abstraction;
using PoolingCore;
using UnityEngine;

namespace Models.Weapons.Base
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Rigidbody2D parentRigidbody;
        [SerializeField] protected WeaponStats stats;
        
        private ObjectPool<Bullet> _bulletsPool;
        
        protected bool IsReloading;
        protected int BulletsInMagazine;
        
        private void Start()
        {
            _bulletsPool = new ObjectPool<Bullet>(bulletPrefab.GetComponent<Bullet>());
            BulletsInMagazine = stats.GetMagazineCapacity();
        }

        private void Update()
        {
            transform.rotation = Quaternion.Euler(0, 0, GetFireDirectionAngle());
            if (GetFirePermission() && !IsReloading && BulletsInMagazine > 0)
            {
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
                bullet.Init(stats);
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