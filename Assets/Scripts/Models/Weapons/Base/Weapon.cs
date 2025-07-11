using Core.Input.Interfaces;
using Models.Bullets.Abstraction;
using Models.Weapons.Data;
using Models.Weapons.Data.WeaponStatsMultipliers;
using Models.Weapons.Data.WeaponStatsMultipliers.Abstraction;
using Models.Weapons.Enums;
using PoolingCore;
using UnityEngine;
using Zenject;

namespace Models.Weapons.Abstraction
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Rigidbody2D parentRigidbody;
        [SerializeField] protected WeaponStats stats;
        
        private ObjectPool<Bullet> _bulletsPool;
        
        protected bool IsReloading;
        protected int BulletsInMagazine;
        
        [Inject]
        private void Construct(
            PlayerShortRangeWeaponStatsMultiplier playerShortRangeWeaponStatsMultiplier,
            PlayerMediumRangeWeaponStatsMultiplier playerMediumRangeWeaponStatsMultiplier, 
            PlayerLongRangeWeaponStatsMultiplier playerLongRangeWeaponStatsMultiplier)
        {
            switch (stats.weaponType)
            {
                case WeaponType.PlayerShortRange:
                    stats.SetStatsMultiplier(playerShortRangeWeaponStatsMultiplier);
                    break;
                case WeaponType.PlayerMediumRange:
                    stats.SetStatsMultiplier(playerMediumRangeWeaponStatsMultiplier);
                    break;
                case WeaponType.PlayerLongRange:
                    stats.SetStatsMultiplier(playerLongRangeWeaponStatsMultiplier);
                    break;
                default:
                    Debug.LogError($"Unknown weapon type: {stats.weaponType}");
                    break;
            }
        }
        
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