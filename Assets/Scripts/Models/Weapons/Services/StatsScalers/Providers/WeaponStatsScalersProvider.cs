using Models.Weapons.Enums;
using Models.Weapons.Services.StatsScalers.Interfaces;

namespace Models.Weapons.Services.StatsScalers.Providers
{
    public class WeaponStatsScalersProvider
    {
        private readonly IWeaponStatsScaler _playerWeaponsStatsScaler = new WeaponStatsScaler();
        private readonly IWeaponStatsScaler _enemyWeaponsStatsScaler = new WeaponStatsScaler();
        private readonly IWeaponStatsScaler _bossWeaponsStatsScaler = new WeaponStatsScaler();

        public IWeaponStatsScaler GetFor(WeaponType weaponType)
        {
            return weaponType switch
            {
                WeaponType.PlayerWeapon => _playerWeaponsStatsScaler,
                WeaponType.EnemyWeapon => _enemyWeaponsStatsScaler,
                WeaponType.BossWeapon => _bossWeaponsStatsScaler,
                _ => null
            };
        }
    }
}