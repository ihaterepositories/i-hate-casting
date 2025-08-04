using System.Collections;
using Core.Input.Interfaces;
using Models.Weapons.Base;
using Models.Weapons.Base.Enums;
using Models.Weapons.Implementations.PlayerWeaponImplementation.StatsMultipliers;
using UnityEngine;
using Zenject;

namespace Models.Weapons.Implementations.PlayerWeaponImplementation
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