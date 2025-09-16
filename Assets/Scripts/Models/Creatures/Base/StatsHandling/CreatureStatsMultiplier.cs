using Models.Creatures.Base.StatsHandling.Enums;
using Models.Creatures.Items.Implementations.Artefacts.Modifiers.Implementations.CreaturesModifierImplementation.ScriptableObjects.SerializingDataContainers;
using UnityEngine;

namespace Models.Creatures.Base.StatsHandling
{
    public class CreatureStatsMultiplier : MonoBehaviour
    {
        private float _healthMultiplier = 1f;
        private float _speedMultiplier = 1f;
        private float _burstDurationMultiplier = 1f;
        private float _whileBurstSpeedMultiplyingMultiplier = 1f;
        private float _burstCooldownMultiplier = 1f;
        
        public float GetMultiplier(CreatureStatType type)
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