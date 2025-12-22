using Models.Creatures.Services.StatsCalculating.StatsModifying.Interfaces;
using Models.Modifiers.CreaturesModifierImpl.DataContainers;

namespace Models.Creatures.Services.StatsCalculating.StatsModifying
{
    public class CreatureStatsModifier : ICreatureStatsModifier
    {
        private float _healthMultiplier = 1f;
        private float _speedMultiplier = 1f;
        private float _boostStrengthMultiplier = 1f;
        private float _boostDurationMultiplier = 1f;
        private float _boostCooldownTimeMultiplier = 1f;
        
        public float ModifyMaxHealth(float baseHealth) => _healthMultiplier * baseHealth;
        public float ModifySpeed(float baseSpeed) => _speedMultiplier * baseSpeed;
        public float ModifyBoostStrength(float baseSpeed) => _boostStrengthMultiplier * baseSpeed;
        public float ModifyBoostDuration(float baseDuration) => _boostDurationMultiplier * baseDuration;
        public float ModifyBoostCooldownTime(float baseCooldown) => _boostCooldownTimeMultiplier * baseCooldown;
        
        public void AddValuesToMultipliers(CreatureModifyingValues modifierValues)
        {
            ChangeMultipliersValues(modifierValues, 1);
        }
        
        public void SubtractValuesFromMultipliers(CreatureModifyingValues modifierValues)
        {
            ChangeMultipliersValues(modifierValues, -1);
        }

        private void ChangeMultipliersValues(CreatureModifyingValues modifierValues, int operation = 1)
        {
            _healthMultiplier += operation * modifierValues.HealthModifier;
            _speedMultiplier += operation * modifierValues.SpeedModifier;
            _boostDurationMultiplier += operation * modifierValues.BurstDurationModifier;
            _boostStrengthMultiplier += operation * modifierValues.WhileBurstSpeedMultiplyingModifier;
            _boostCooldownTimeMultiplier += operation * modifierValues.BurstCooldown;
        }
    }
}