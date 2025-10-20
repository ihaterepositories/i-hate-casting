using Models.Items.Modifiers.CreaturesModifierImpl.DataContainers;
using Models.WorldObjects.Creatures.Base.StatsHandling.Enums;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.StatsHandling
{
    public class CreatureStatsMultiplier
    {
        private float _healthMultiplier = 1f;
        private float _speedMultiplier = 1f;
        private float _burstDurationMultiplier = 1f;
        private float _whileBurstSpeedMultiplyingMultiplier = 1f;
        private float _burstCooldownMultiplier = 1f;
        
        public float Multiply(CreatureStatType type)
        {
            return type switch
            {
                CreatureStatType.MaxHealth => _healthMultiplier,
                CreatureStatType.Speed => _speedMultiplier,
                CreatureStatType.BurstDuration => _burstDurationMultiplier,
                CreatureStatType.WhileBurstSpeedIncreaseCoefficient => _whileBurstSpeedMultiplyingMultiplier,
                CreatureStatType.BurstCooldown => _burstCooldownMultiplier,
                _ => 1f
            };
        }
        
        public void AddValuesToMultiplier(CreatureModifyingValues modifierValues)
        {
            if (modifierValues == null)
            {
                Debug.LogWarning("Creature stats is null. Cannot add values to multiplier.");
                return;
            }
            
            _healthMultiplier += modifierValues.HealthModifier;
            _speedMultiplier += modifierValues.SpeedModifier;
            _burstDurationMultiplier += modifierValues.BurstDurationModifier;
            _whileBurstSpeedMultiplyingMultiplier += modifierValues.WhileBurstSpeedMultiplyingModifier;
            _burstCooldownMultiplier += modifierValues.BurstCooldown;
        }
        
        public void SubtractValuesFromMultiplier(CreatureModifyingValues modifierValues)
        {
            if (modifierValues == null)
            {
                Debug.LogWarning("Creature stats is null. Cannot subtract values from multiplier.");
                return;
            }
            
            _healthMultiplier -= modifierValues.HealthModifier;
            _speedMultiplier -= modifierValues.SpeedModifier;
            _burstDurationMultiplier -= modifierValues.BurstDurationModifier;
            _whileBurstSpeedMultiplyingMultiplier -= modifierValues.WhileBurstSpeedMultiplyingModifier;
            _burstCooldownMultiplier -= modifierValues.BurstCooldown;
        }
    }
}