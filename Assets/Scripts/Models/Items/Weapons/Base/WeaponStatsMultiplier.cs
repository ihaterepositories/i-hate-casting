using Models.Items.Modifiers.Implementations.WeaponsModifierImplementation.Models;
using Models.Items.Weapons.Base.Enums;
using UnityEngine;

namespace Models.Items.Weapons.Base
{
    public class WeaponStatsMultiplier : MonoBehaviour
    {
        private float _reloadTimeMultiplier = 1f;
        private float _spreadMultiplier = 1f;
        private float _damageMultiplier = 1;
        private float _speedMultiplier = 1f;
        private float _cooldownTimeMultiplier = 1f;
        
        // ???
        //public float LifeTimeMultiplier = 1f;
        
        public float GetMultiplier(WeaponStatType type)
        {
            return type switch
            {
                WeaponStatType.ReloadTime => _reloadTimeMultiplier,
                WeaponStatType.Spread => _spreadMultiplier,
                WeaponStatType.Damage => _damageMultiplier,
                WeaponStatType.Speed => _speedMultiplier,
                WeaponStatType.CooldownTime => _cooldownTimeMultiplier,
                _ => 1f
            };
        }
        
        public void AddValueToMultipliers(ModifiableWeaponStats modifiableWeaponsModifierStats)
        {
            if (modifiableWeaponsModifierStats == null)
            {
                Debug.LogWarning("Weapon modifier stats is null. Cannot multiply stats.");
                return;
            }
            
            _reloadTimeMultiplier += modifiableWeaponsModifierStats.ReloadTimeModifier;
            _spreadMultiplier += modifiableWeaponsModifierStats.SpreadModifier;
            _damageMultiplier += modifiableWeaponsModifierStats.DamageModifier;
            _speedMultiplier += modifiableWeaponsModifierStats.SpeedModifier;
            _cooldownTimeMultiplier += modifiableWeaponsModifierStats.CooldownTimeModifier;
        }

        public void SubtractValueFromMultipliers(ModifiableWeaponStats modifiableWeaponsModifierStats)
        {
            if (modifiableWeaponsModifierStats == null)
            {
                Debug.LogWarning("Weapon modifier stats is null. Cannot subtract stats.");
                return;
            }

            _reloadTimeMultiplier -= modifiableWeaponsModifierStats.ReloadTimeModifier;
            _spreadMultiplier -= modifiableWeaponsModifierStats.SpreadModifier;
            _damageMultiplier -= modifiableWeaponsModifierStats.DamageModifier;
            _speedMultiplier -= modifiableWeaponsModifierStats.SpeedModifier;
            _cooldownTimeMultiplier -= modifiableWeaponsModifierStats.CooldownTimeModifier;
        }
    }
}