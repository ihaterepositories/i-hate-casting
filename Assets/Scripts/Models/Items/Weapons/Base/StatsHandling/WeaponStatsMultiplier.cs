using Models.Items.Artefacts.Modifiers.Implementations.WeaponsModifierImplementation.ScriptableObjects.SerializingDataContainers;
using Models.Items.Weapons.Base.StatsHandling.Enums;
using UnityEngine;

namespace Models.Items.Weapons.Base.StatsHandling
{
    public class WeaponStatsMultiplier : MonoBehaviour
    {
        private float _reloadTimeMultiplier = 1f;
        private float _spreadMultiplier = 1f;
        private float _damageToDealMultiplier = 1;
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
                WeaponStatType.DamageToDeal => _damageToDealMultiplier,
                WeaponStatType.Speed => _speedMultiplier,
                WeaponStatType.CooldownTime => _cooldownTimeMultiplier,
                _ => 1f
            };
        }
        
        public void AddValuesToMultipliers(WeaponModifyingValues modifyingValuesToModify)
        {
            if (modifyingValuesToModify == null)
            {
                Debug.LogWarning("Weapon modifier stats is null. Cannot multiply stats.");
                return;
            }
            
            _reloadTimeMultiplier += modifyingValuesToModify.ReloadTimeModifier;
            _spreadMultiplier += modifyingValuesToModify.SpreadModifier;
            _damageToDealMultiplier += modifyingValuesToModify.DamageToDealModifier;
            _speedMultiplier += modifyingValuesToModify.SpeedModifier;
            _cooldownTimeMultiplier += modifyingValuesToModify.CooldownTimeModifier;
        }

        public void SubtractValueFromMultipliers(WeaponModifyingValues modifyingValuesToModify)
        {
            if (modifyingValuesToModify == null)
            {
                Debug.LogWarning("Weapon modifier stats is null. Cannot subtract stats.");
                return;
            }

            _reloadTimeMultiplier -= modifyingValuesToModify.ReloadTimeModifier;
            _spreadMultiplier -= modifyingValuesToModify.SpreadModifier;
            _damageToDealMultiplier -= modifyingValuesToModify.DamageToDealModifier;
            _speedMultiplier -= modifyingValuesToModify.SpeedModifier;
            _cooldownTimeMultiplier -= modifyingValuesToModify.CooldownTimeModifier;
        }
    }
}