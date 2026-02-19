using System;
using Models.Weapons.Dtos;
using Models.Weapons.Enums;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using Models.Weapons.Services.StatsModifying.Providers;

namespace Models.Weapons.Services.StatsCalculating.Factories
{
    public class WeaponStatsCalculatorsFactory
    {
        private readonly WeaponStatsModifiersProvider _statsModifiersProvider;

        public WeaponStatsCalculatorsFactory(WeaponStatsModifiersProvider statsModifiersProvider)
        {
            _statsModifiersProvider = statsModifiersProvider;
        }

        public IWeaponStatsCalculator Create(WeaponType weaponType, WeaponStats stats)
        {
            return weaponType switch
            {
                WeaponType.PlayerWeapon => new WeaponStatsCalculator(stats, _statsModifiersProvider.GetFor(weaponType)),
                WeaponType.EnemyWeapon => new WeaponStatsCalculator(stats, _statsModifiersProvider.GetFor(weaponType)),
                WeaponType.BossWeapon => new WeaponStatsCalculator(stats, _statsModifiersProvider.GetFor(weaponType)),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}