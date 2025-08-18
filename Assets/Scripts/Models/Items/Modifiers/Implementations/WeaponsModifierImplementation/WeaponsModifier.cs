using Models.Items.Modifiers.Base;
using Models.Items.Modifiers.Implementations.WeaponsModifierImplementation.Models;
using Models.Items.Modifiers.Implementations.WeaponsModifierImplementation.ScriptableObjects;
using Models.Items.Weapons.Base;
using Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation.StatsMultipliers;
using UnityEngine;
using Zenject;

namespace Models.Items.Modifiers.Implementations.WeaponsModifierImplementation
{
    public class WeaponsModifier : Modifier
    {
        [SerializeField] private WeaponsModifierStatsSO _weaponsModifyingStats;

        private (WeaponStatsMultiplier multiplier, ModifiableWeaponStats stats)[] _weaponData;

        [Inject]
        private void Construct(
            PlayerShortRangeWeaponStatsMultiplier shortRange,
            PlayerMediumRangeWeaponStatsMultiplier mediumRange,
            PlayerLongRangeWeaponStatsMultiplier longRange)
        {
            _weaponData = new (WeaponStatsMultiplier, ModifiableWeaponStats)[]
            {
                (shortRange, _weaponsModifyingStats.PlayerShortRangeModifiableWeaponStats),
                (mediumRange, _weaponsModifyingStats.PlayerMediumRangeModifiableWeaponStats),
                (longRange, _weaponsModifyingStats.PlayerLongRangeModifiableWeaponStats)
            };
        }

        protected override void ActivateModifier()
        {
            foreach (var (multiplier, stats) in _weaponData)
            {
                multiplier.AddValueToMultipliers(stats);
            }
        }

        protected override void DeactivateModifier()
        {
            foreach (var (multiplier, stats) in _weaponData)
            {
                multiplier.SubtractValueFromMultipliers(stats);
            }
        }
    }
}