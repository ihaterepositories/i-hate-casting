using Models.Items.Weapons.Base.Enums;

namespace Models.Items.Weapons.Base.StatsHandling.Providers
{
    public class WeaponStatsMultipliersProvider
    {
        private readonly WeaponStatsMultiplier _playerShortRangeWeaponStatsMultiplier;
        private readonly WeaponStatsMultiplier _playerMediumRangeWeaponStatsMultiplier;
        private readonly WeaponStatsMultiplier _playerLongRangeWeaponStatsMultiplier;
        private readonly WeaponStatsMultiplier _defaultEnemyWeaponsStatsMultiplier;
        private readonly WeaponStatsMultiplier _bossWeaponsStatsMultiplier;
        
        public WeaponStatsMultipliersProvider()
        {
            _playerShortRangeWeaponStatsMultiplier = new WeaponStatsMultiplier();
            _playerMediumRangeWeaponStatsMultiplier = new WeaponStatsMultiplier();
            _playerLongRangeWeaponStatsMultiplier = new WeaponStatsMultiplier();
            _defaultEnemyWeaponsStatsMultiplier = new WeaponStatsMultiplier();
            _bossWeaponsStatsMultiplier = new WeaponStatsMultiplier();
        }
        
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