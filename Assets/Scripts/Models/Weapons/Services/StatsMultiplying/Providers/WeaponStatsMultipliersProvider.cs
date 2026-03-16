using Models.Weapons.Enums;
using Models.Weapons.Services.StatsMultiplying.Interfaces;

namespace Models.Weapons.Services.StatsMultiplying.Providers
{
    public class WeaponStatsMultipliersProvider
    {
        private readonly IWeaponStatsMultipliers _playerWeaponStatsMultipliers = new WeaponStatsMultipliers();
        private readonly IWeaponStatsMultipliers _defaultEnemyWeaponsStatsMultipliers = new WeaponStatsMultipliers();
        private readonly IWeaponStatsMultipliers _bossWeaponsStatsMultipliers = new WeaponStatsMultipliers();

        public IWeaponStatsMultipliers GetFor(WeaponType weaponType)
        {
            return weaponType switch
            {
                WeaponType.PlayerWeapon => _playerWeaponStatsMultipliers,
                WeaponType.EnemyWeapon => _defaultEnemyWeaponsStatsMultipliers,
                WeaponType.BossWeapon => _bossWeaponsStatsMultipliers,
                _ => null
            };
        }
    }
}