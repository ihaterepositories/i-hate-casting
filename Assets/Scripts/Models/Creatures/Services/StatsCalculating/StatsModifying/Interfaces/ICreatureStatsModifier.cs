using Models.Modifiers.CreaturesModifierImpl.DataContainers;

namespace Models.Creatures.Services.StatsCalculating.StatsModifying.Interfaces
{
    public interface ICreatureStatsModifier
    {
        public float ModifyMaxHealth(float baseHealth);
        public float ModifySpeed(float baseSpeed);
        public float ModifyBoostStrength(float baseSpeed);
        public float ModifyBoostDuration(float baseDuration);
        public float ModifyBoostCooldownTime(float baseCooldown);

        public void AddValuesToMultipliers(CreatureModifyingValues modifierValues);
        public void SubtractValuesFromMultipliers(CreatureModifyingValues modifierValues);
    }
}