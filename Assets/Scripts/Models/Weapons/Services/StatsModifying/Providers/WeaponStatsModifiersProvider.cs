using Models.Weapons.Enums;
using Models.Weapons.Services.StatsModifying.Interfaces;

namespace Models.Weapons.Services.StatsModifying.Providers
{
    public class WeaponStatsModifiersProvider
    {
        private readonly IWeaponStatsModifier _playerWeaponStatsModifier = new WeaponStatsModifier();
        private readonly IWeaponStatsModifier _defaultEnemyWeaponsStatsModifier = new WeaponStatsModifier();
        private readonly IWeaponStatsModifier _bossWeaponsStatsModifier = new WeaponStatsModifier();

        public IWeaponStatsModifier GetFor(WeaponType weaponType)
        {
            return weaponType switch
            {
                WeaponType.PlayerWeapon => _playerWeaponStatsModifier,
                WeaponType.EnemyWeapon => _defaultEnemyWeaponsStatsModifier,
                WeaponType.BossWeapon => _bossWeaponsStatsModifier,
                _ => null
            };
        }
    }
}