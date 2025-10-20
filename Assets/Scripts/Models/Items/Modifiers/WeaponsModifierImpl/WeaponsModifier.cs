using Models.Items.Modifiers.Base;
using Models.Items.Modifiers.WeaponsModifierImpl.DataContainers;
using Models.Items.Weapons.Base.Enums;
using Models.Items.Weapons.Base.StatsHandling;
using Models.Items.Weapons.Base.StatsHandling.Providers;
using UnityEngine;
using Zenject;

namespace Models.Items.Modifiers.WeaponsModifierImpl
{
    public class WeaponsModifier : Modifier
    {
        [SerializeField] private WeaponsModifierStats _weaponsModifierStats;

        private WeaponStatsMultipliersProvider _weaponsStatsMultipliersProvider;
        private (WeaponStatsMultiplier multiplier, WeaponModifyingValues modifyingValues)[] _weaponsModifyingData;

        [Inject]
        private void Construct(WeaponStatsMultipliersProvider weaponStatsMultipliersProvider)
        {
            _weaponsStatsMultipliersProvider = weaponStatsMultipliersProvider;
        }

        private void Awake()
        {
            _weaponsModifyingData = new[]
            {
                (_weaponsStatsMultipliersProvider.GetFor(WeaponType.PlayerShortRange), _weaponsModifierStats.PlayerShortRangeWeaponModifyingValues),
                (_weaponsStatsMultipliersProvider.GetFor(WeaponType.PlayerMediumRange), _weaponsModifierStats.PlayerMediumRangeWeaponModifyingValues),
                (_weaponsStatsMultipliersProvider.GetFor(WeaponType.PlayerLongRange), _weaponsModifierStats.PlayerLongRangeWeaponModifyingValues),
                (_weaponsStatsMultipliersProvider.GetFor(WeaponType.DefaultEnemyWeapon), _weaponsModifierStats.DefaultEnemyWeaponModifyingValues),
                (_weaponsStatsMultipliersProvider.GetFor(WeaponType.BossWeapon), _weaponsModifierStats.BossWeaponModifyingValues)
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
                multiplier.SubtractValueFromMultipliers(stats);
            }
        }
    }
}