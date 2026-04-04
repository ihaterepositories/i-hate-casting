using Models.Creatures.Dtos;
using Models.Creatures.Services.StatsScalers.Interfaces;

namespace Models.Creatures.Services.StatsScalers
{
    public class CreatureStatsScaler : ICreatureStatsScaler
    {
        private float _maxHealthMultiplier = 1f;
        private float _speedMultiplier = 1f;
        private float _boostStrengthMultiplier = 1f;
        private float _boostDurationMultiplier = 1f;
        private float _boostCooldownTimeMultiplier = 1f;
        
        public float ScaleMaxHealth(float baseHealth) => _maxHealthMultiplier * baseHealth;
        public float ScaleSpeed(float baseSpeed) => _speedMultiplier * baseSpeed;
        public float ScaleBoostStrength(float baseBoostStrength) => _boostStrengthMultiplier * baseBoostStrength;
        public float ScaleBoostDuration(float baseDuration) => _boostDurationMultiplier * baseDuration;
        public float ScaleBoostCooldownTime(float baseCooldown) => _boostCooldownTimeMultiplier * baseCooldown;
        
        public void AddMultipliers(CreatureStats valuesToAdd)
        {
            ChangeMultipliersValues(valuesToAdd, 1);
        }
        
        public void RemoveMultipliers(CreatureStats valuesToSubtract)
        {
            ChangeMultipliersValues(valuesToSubtract, -1);
        }

        private void ChangeMultipliersValues(CreatureStats modifiers, int operation = 1)
        {
            _maxHealthMultiplier += operation * modifiers.MaxHealth;
            _speedMultiplier += operation * modifiers.Speed;
            _boostDurationMultiplier += operation * modifiers.BoostDuration;
            _boostStrengthMultiplier += operation * modifiers.BoostStrength;
            _boostCooldownTimeMultiplier += operation * modifiers.BoostCooldownTime;
        }
    }
}