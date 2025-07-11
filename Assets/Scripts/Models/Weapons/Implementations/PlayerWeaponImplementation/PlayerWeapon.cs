using System.Collections;
using Core.Input.Interfaces;
using Models.Weapons.Abstraction;
using Models.Weapons.Data.WeaponStatsMultipliers;
using Models.Weapons.Enums;
using UnityEngine;
using Zenject;

namespace Models.Weapons
{
    public class PlayerWeapon : Weapon
    {
        private IInputHandler _inputHandler;
        
        [Inject]
        private void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        protected override bool GetFirePermission()
        {
            return _inputHandler.IsShootButtonPressed();
        }

        protected override bool GetReloadPermission()
        {
            return _inputHandler.IsReloadButtonPressed();
        }

        protected override void Reload()
        {
            StartCoroutine(ReloadCoroutine());
        }
        
        private IEnumerator ReloadCoroutine()
        {
            Debug.Log("Starting reload...");
            IsReloading = true;
            yield return new WaitForSeconds(stats.GetReloadTime());
            BulletsInMagazine = stats.GetMagazineCapacity();
            IsReloading = false;
            Debug.Log("Reload complete.");
        }

        protected override float GetFireDirectionAngle()
        {
            Vector2 lookDirection = (_inputHandler.GetPointerPosition() - transform.position);
            return Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + GetSpread();
        }

        private float GetSpread()
        {
            return Random.Range(-stats.GetSpread(),
                stats.GetSpread());
        }
    }
}