using Models.Modifiers.Base;
using Models.Modifiers.WeaponsModifierImpl.DataContainers;
using Models.Weapons.Enums;
using Models.Weapons.Services.StatsModifying.Interfaces;
using Models.Weapons.Services.StatsModifying.Providers;
using UnityEngine;
using Zenject;

namespace Models.Modifiers.WeaponsModifierImpl
{
    public class WeaponsModifier : Modifier
    {
        [SerializeField] private WeaponsModifierStats _weaponsModifierStats;

        private WeaponStatsModifiersProvider _weaponsStatsModifiersProvider;
        private (IWeaponStatsModifier multiplier, WeaponModifyingValues modifyingValues)[] _weaponsModifyingData;

        [Inject]
        private void Construct(WeaponStatsModifiersProvider weaponStatsModifiersProvider)
        {
            _weaponsStatsModifiersProvider = weaponStatsModifiersProvider;
        }

        private void Awake()
        {
            _weaponsModifyingData = new[]
            {
                (_weaponsStatsModifiersProvider.GetFor(WeaponType.PlayerShortRange), _weaponsModifierStats.PlayerShortRangeWeaponModifyingValues),
                (_weaponsStatsModifiersProvider.GetFor(WeaponType.PlayerMediumRange), _weaponsModifierStats.PlayerMediumRangeWeaponModifyingValues),
                (_weaponsStatsModifiersProvider.GetFor(WeaponType.PlayerLongRange), _weaponsModifierStats.PlayerLongRangeWeaponModifyingValues),
                (_weaponsStatsModifiersProvider.GetFor(WeaponType.EnemyWeapon), _weaponsModifierStats.DefaultEnemyWeaponModifyingValues),
                (_weaponsStatsModifiersProvider.GetFor(WeaponType.BossWeapon), _weaponsModifierStats.BossWeaponModifyingValues)
            };
        }

        protected override void ActivateModifier()
        {
            foreach (var (multiplier, stats) in _weaponsModifyingData)
            {
                multiplier.AddValuesToMultipliers(stats);
            }
        }

        protected override void DeactivateModifier()
        {
            foreach (var (multiplier, stats) in _weaponsModifyingData)
            {
                multiplier.SubtractValuesFromMultipliers(stats);
            }
        }
    }
}