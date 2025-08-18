using Core.Input.Interfaces;
using Models.Items.Weapons.Base;
using Models.Items.Weapons.Base.Enums;
using Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation.StatsMultipliers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation
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
            
            switch (_weaponStats.WeaponType)
            {
                case WeaponType.PlayerShortRange:
                    _weaponStats.SetStatsMultiplier(playerShortRangeWeaponStatsMultiplier);
                    break;
                case WeaponType.PlayerMediumRange:
                    _weaponStats.SetStatsMultiplier(playerMediumRangeWeaponStatsMultiplier);
                    break;
                case WeaponType.PlayerLongRange:
                    _weaponStats.SetStatsMultiplier(playerLongRangeWeaponStatsMultiplier);
                    break;
                default:
                    Debug.LogError($"Unknown weapon type: {_weaponStats.WeaponType}");
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

        protected override float GetFireDirectionAngle()
        {
            Vector2 lookDirection = (_inputHandler.GetPointerPosition() - transform.position);
            return Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + GetSpread();
        }

        private float GetSpread()
        {
            return Random.Range(-_weaponStats.GetSpread(),
                _weaponStats.GetSpread());
        }
    }
}