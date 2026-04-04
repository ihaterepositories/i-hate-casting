using Models.Weapons.Dtos;

namespace Models.Weapons.Services.StatsScalers.Interfaces
{
    /// <summary>
    /// Applies and manages stat multipliers for a weapon.
    /// Scales base stats with values given from currently active stats modifiers (game buffs/events).
    ///
    /// Do not create instance! Take it from provider.
    /// </summary>
    public interface IWeaponStatsScaler
    {
        public float ScaleReloadTime(float baseReloadTime);
        public float ScaleSpreadDegree(float baseSpread);
        public float ScaleDamageToDeal(float baseDamage);
        public float ScaleSpeed(float baseSpeed);
        public float ScaleCooldownTime(float baseCooldownTime);

        /// <summary>
        /// Adds the given values to the set of multipliers.
        /// </summary>
        public void AddMultipliers(WeaponStats valuesToAdd);
        
        /// <summary>
        /// Removes the given values from the set of multipliers.
        /// </summary>
        public void RemoveMultipliers(WeaponStats valuesToSubtract);
    }
}