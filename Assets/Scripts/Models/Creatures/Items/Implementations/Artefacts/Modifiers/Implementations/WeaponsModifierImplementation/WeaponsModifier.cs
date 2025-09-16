using Models.Creatures.Items.Implementations.Artefacts.Modifiers.Base;
using Models.Creatures.Items.Implementations.Artefacts.Modifiers.Implementations.WeaponsModifierImplementation.ScriptableObjects;
using Models.Creatures.Items.Implementations.Artefacts.Modifiers.Implementations.WeaponsModifierImplementation.ScriptableObjects.SerializingDataContainers;
using Models.Creatures.Items.Implementations.Weapons.Base.Enums;
using Models.Creatures.Items.Implementations.Weapons.Base.StatsHandling;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Items.Implementations.Artefacts.Modifiers.Implementations.WeaponsModifierImplementation
{
    public class WeaponsModifier : Modifier
    {
        [SerializeField] private WeaponsModifierStatsSo _weaponsModifierStats;

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