using Models.Weapons.Enums;
using Models.Weapons.Services.StatsModifying.Interfaces;

namespace Models.Weapons.Services.StatsModifying.Providers
{
    public class WeaponStatsModifiersProvider
    {
        private readonly IWeaponStatsModifier _playerShortRangeWeaponStatsModifier;
        private readonly IWeaponStatsModifier _playerMediumRangeWeaponStatsModifier;
        private readonly IWeaponStatsModifier _playerLongRangeWeaponStatsModifier;
        private readonly IWeaponStatsModifier _defaultEnemyWeaponsStatsModifier;
        private readonly IWeaponStatsModifier _bossWeaponsStatsModifier;
        
        public WeaponStatsModifiersProvider()
        {
            _playerShortRangeWeaponStatsModifier = new WeaponStatsModifier();
            _playerMediumRangeWeaponStatsModifier = new WeaponStatsModifier();
            _playerLongRangeWeaponStatsModifier = new WeaponStatsModifier();
            _defaultEnemyWeaponsStatsModifier = new WeaponStatsModifier();
            _bossWeaponsStatsModifier = new WeaponStatsModifier();
        }
        
        public IWeaponStatsModifier GetFor(WeaponType weaponType)
        {
            return weaponType switch
            {
                WeaponType.PlayerShortRange => _playerShortRangeWeaponStatsModifier,
                WeaponType.PlayerMediumRange => _playerMediumRangeWeaponStatsModifier,
                WeaponType.PlayerLongRange => _playerLongRangeWeaponStatsModifier,
                WeaponType.EnemyWeapon => _defaultEnemyWeaponsStatsModifier,
                WeaponType.BossWeapon => _bossWeaponsStatsModifier,
                _ => null
            };
        }
    }
}