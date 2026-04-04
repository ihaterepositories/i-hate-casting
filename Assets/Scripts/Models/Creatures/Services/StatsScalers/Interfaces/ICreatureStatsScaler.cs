using Models.Creatures.Dtos;

namespace Models.Creatures.Services.StatsScalers.Interfaces
{
    /// <summary>
    /// Applies and manages stat multipliers for a creature.
    /// Scales base stats with values given from currently active stats modifiers (game buffs/events).
    ///
    /// Do not create instance! Take it from provider.
    /// </summary>
    public interface ICreatureStatsScaler
    {
        public float ScaleMaxHealth(float baseHealth);
        public float ScaleSpeed(float baseSpeed);
        public float ScaleBoostStrength(float baseBoostStrength);
        public float ScaleBoostDuration(float baseDuration);
        public float ScaleBoostCooldownTime(float baseCooldown);

        /// <summary>
        /// Adds the given values to the set of multipliers.
        /// </summary>
        public void AddMultipliers(CreatureStats valuesToAdd);
        
        /// <summary>
        /// Removes the given values from the set of multipliers.
        /// </summary>
        public void RemoveMultipliers(CreatureStats valuesToSubtract);
    }
}