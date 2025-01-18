using System.Collections;
using Core.Input.Interfaces;
using Models.Weapons.PlayerWeapon.Data;
using UnityEngine;
using Zenject;

namespace Models.Weapons.PlayerWeapon
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        
        private bool _isReloading;
        private IInputHandler _inputHandler;
        private int _bulletsInMagazine = PlayerWeaponStats.MagazineCapacity;
        
        private float ReloadTime => PlayerWeaponStats.ReloadTime;
        private float Spread => PlayerWeaponStats.Spread;
        private int MagazineCapacity => PlayerWeaponStats.MagazineCapacity;
        
        [Inject]
        private void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        private void Update()
        {
            if (_inputHandler.IsShootButtonPressed() && !_isReloading)
            {
                Shoot();
                
                if (_bulletsInMagazine == 0)
                    StartCoroutine(Reload());
            }
        }

        private void Shoot()
        {
            var pointerPosition = _inputHandler.GetPointerPosition();
            var lookDirection = (pointerPosition - transform.position).normalized;
            var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + Random.Range(-Spread, Spread);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
            _bulletsInMagazine--;
        }
        
        private IEnumerator Reload()
        {
            _isReloading = true;
            yield return new WaitForSeconds(ReloadTime);
            _bulletsInMagazine = MagazineCapacity;
            _isReloading = false;
        }
    }
}