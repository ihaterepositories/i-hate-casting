using System.Collections;
using Core.Input.Interfaces;
using Models.Items.Weapons.Base;
using Models.Items.Weapons.Base.Enums;
using Models.Items.Weapons.Implementations.PlayerWeaponImplementation.StatsMultipliers;
using UnityEngine;
using Zenject;

namespace Models.Items.Weapons.Implementations.PlayerWeaponImplementation
{
    public class PlayerWeapon : Weapon
    {
        private IInputHandler _inputHandler;
        
        [Inject]
        private void Construct(
            IInputHandler inputHandler,
            PlayerShortRangeWeaponStatsMultiplier playerShortRangeWeaponStatsMultiplier,
            PlayerMediumRangeWeaponStatsMultiplier playerMediumRangeWeaponStatsMultiplier, 
            PlayerLongRangeWeaponStatsMultiplier playerLongRangeWeaponStatsMultiplier)
        {
            _inputHandler = inputHandler;
            
            switch (weaponStats.weaponType)
            {
                case WeaponType.PlayerShortRange:
                    weaponStats.SetStatsMultiplier(playerShortRangeWeaponStatsMultiplier);
                    break;
                case WeaponType.PlayerMediumRange:
                    weaponStats.SetStatsMultiplier(playerMediumRangeWeaponStatsMultiplier);
                    break;
                case WeaponType.PlayerLongRange:
                    weaponStats.SetStatsMultiplier(playerLongRangeWeaponStatsMultiplier);
                    break;
                default:
                    Debug.LogError($"Unknown weapon type: {weaponStats.weaponType}");
                    break;
            }
        }

        protected override bool GetFirePermission()
        {
            return _inputHandler.IsFireButtonPressed();
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
            yield return new WaitForSeconds(weaponStats.GetReloadTime());
            BulletsInMagazine = weaponStats.GetMagazineCapacity();
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
            return Random.Range(-weaponStats.GetSpread(),
                weaponStats.GetSpread());
        }
    }
}