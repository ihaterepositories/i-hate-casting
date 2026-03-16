using System;
using Models.Weapons.Dtos;
using Models.Weapons.Enums;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using Models.Weapons.Services.StatsMultiplying.Providers;

namespace Models.Weapons.Services.StatsCalculating.Factories
{
    public class WeaponStatsCalculatorsFactory
    {
        private readonly WeaponStatsMultipliersProvider _statsMultipliersProvider;

        public WeaponStatsCalculatorsFactory(WeaponStatsMultipliersProvider statsMultipliersProvider)
        {
            _statsMultipliersProvider = statsMultipliersProvider;
        }

        public IWeaponStatsCalculator Create(WeaponType weaponType, WeaponStats stats)
        {
            return weaponType switch
            {
                WeaponType.PlayerWeapon => new WeaponStatsCalculator(stats, _statsMultipliersProvider.GetFor(weaponType)),
                WeaponType.EnemyWeapon => new WeaponStatsCalculator(stats, _statsMultipliersProvider.GetFor(weaponType)),
                WeaponType.BossWeapon => new WeaponStatsCalculator(stats, _statsMultipliersProvider.GetFor(weaponType)),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}