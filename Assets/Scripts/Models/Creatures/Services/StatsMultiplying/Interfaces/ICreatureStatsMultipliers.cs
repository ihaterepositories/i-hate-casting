using Models.Creatures.Dtos;

namespace Models.Creatures.Services.StatsMultiplying.Interfaces
{
    public interface ICreatureStatsMultipliers
    {
        public float ModifyMaxHealth(float baseHealth);
        public float ModifySpeed(float baseSpeed);
        public float ModifyBoostStrength(float baseSpeed);
        public float ModifyBoostDuration(float baseDuration);
        public float ModifyBoostCooldownTime(float baseCooldown);

        public void AddValuesToMultipliers(CreatureStats modifiers);
        public void SubtractValuesFromMultipliers(CreatureStats modifiers);
    }
}