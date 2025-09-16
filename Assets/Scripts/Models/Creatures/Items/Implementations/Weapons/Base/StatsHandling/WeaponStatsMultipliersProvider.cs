using Models.Creatures.Items.Implementations.Weapons.Base.Enums;
using UnityEngine;

namespace Models.Creatures.Items.Implementations.Weapons.Base.StatsHandling
{
    public class WeaponStatsMultipliersProvider : MonoBehaviour
    {
        [SerializeField] private WeaponStatsMultiplier _playerShortRangeWeaponStatsMultiplier;
        [SerializeField] private WeaponStatsMultiplier _playerMediumRangeWeaponStatsMultiplier;
        [SerializeField] private WeaponStatsMultiplier _playerLongRangeWeaponStatsMultiplier;
        [SerializeField] private WeaponStatsMultiplier _defaultEnemyWeaponsStatsMultiplier;
        [SerializeField] private WeaponStatsMultiplier _bossWeaponsStatsMultiplier;
        
        public WeaponStatsMultiplier GetFor(WeaponType weaponType)
        {
            return weaponType switch
            {
                WeaponType.PlayerShortRange => _playerShortRangeWeaponStatsMultiplier,
                WeaponType.PlayerMediumRange => _playerMediumRangeWeaponStatsMultiplier,
                WeaponType.PlayerLongRange => _playerLongRangeWeaponStatsMultiplier,
                WeaponType.DefaultEnemyWeapon => _defaultEnemyWeaponsStatsMultiplier,
                WeaponType.BossWeapon => _bossWeaponsStatsMultiplier,
                _ => null
            };
        }
    }
}