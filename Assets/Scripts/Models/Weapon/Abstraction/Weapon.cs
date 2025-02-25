using System.Collections;
using Models.Weapon.Data;
using UnityEngine;

namespace Models.Weapon.Abstraction
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private WeaponStats stats;

        private bool _isReloading;
        private int _bulletsInMagazine;

        private void Start()
        {
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
            GameObject bullet = Instantiate(this.bullet, transform.position, Quaternion.Euler(0, 0, GetBulletRotationAngle()));
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