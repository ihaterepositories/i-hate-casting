using Models.Creatures.Dtos;
using Models.Creatures.Services.StatsMultiplying.Interfaces;

namespace Models.Creatures.Services.StatsMultiplying
{
    public class CreatureStatsMultipliers : ICreatureStatsMultipliers
    {
        private float _maxHealthMultiplier = 1f;
        private float _speedMultiplier = 1f;
        private float _boostStrengthMultiplier = 1f;
        private float _boostDurationMultiplier = 1f;
        private float _boostCooldownTimeMultiplier = 1f;
        
        public float ModifyMaxHealth(float baseHealth) => _maxHealthMultiplier * baseHealth;
        public float ModifySpeed(float baseSpeed) => _speedMultiplier * baseSpeed;
        public float ModifyBoostStrength(float baseSpeed) => _boostStrengthMultiplier * baseSpeed;
        public float ModifyBoostDuration(float baseDuration) => _boostDurationMultiplier * baseDuration;
        public float ModifyBoostCooldownTime(float baseCooldown) => _boostCooldownTimeMultiplier * baseCooldown;
        
        public void AddValuesToMultipliers(CreatureStats modifiers)
        {
            ChangeMultipliersValues(modifiers, 1);
        }
        
        public void SubtractValuesFromMultipliers(CreatureStats modifiers)
        {
            ChangeMultipliersValues(modifiers, -1);
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