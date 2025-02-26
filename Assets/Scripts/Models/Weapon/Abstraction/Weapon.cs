using System.Collections;
using Models.Weapon.Bullets;
using Models.Weapon.Data;
using Pooling;
using UnityEngine;

namespace Models.Weapon.Abstraction
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private WeaponStats stats;
        
        private bool _isReloading;
        private int _bulletsInMagazine;

        private ObjectPool<Bullet> _pool;

        private void Start()
        {
            _pool = new ObjectPool<Bullet>(bulletPrefab.GetComponent<Bullet>());
            _bulletsInMagazine = stats.magazineCapacity;
        }

        private void Update()
        {
            if (GetFirePermission() && !_isReloading)
            {
                Fire();
                
                _bulletsInMagazine--;
                if (_bulletsInMagazine == 0)
                {
                    StartCoroutine(Reload());
                }
            }
        }

        protected abstract bool GetFirePermission();
        protected abstract Vector3 GetTargetPosition();

        private void Fire()
        {
            if (_pool.GetFreeObject() is Bullet bullet)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, GetBulletRotationAngle());
            }
            else
            {
                Debug.LogError($"Can`t create bullet in {gameObject.name}.");
            }
        }

        private float GetBulletRotationAngle()
        {
            var lookDirection = (GetTargetPosition() - transform.position).normalized;
            return Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + Random.Range(-stats.spread, stats.spread);
        }
        
        private IEnumerator Reload()
        {
            _isReloading = true;
            yield return new WaitForSeconds(stats.reloadTime);
            _bulletsInMagazine = stats.magazineCapacity;
            _isReloading = false;
        }
    }
}