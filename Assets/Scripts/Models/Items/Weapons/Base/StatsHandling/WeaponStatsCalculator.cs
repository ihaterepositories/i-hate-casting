using Models.Items.Weapons.Base.StatsHandling.DataContainers;
using Models.Items.Weapons.Base.StatsHandling.Enums;
using UnityEngine;

namespace Models.Items.Weapons.Base.StatsHandling
{
    /// <summary>
    /// Util class, calculates weapon stats based on base stats and stats multiplying.
    /// </summary>
    public class WeaponStatsCalculator
    {
        private readonly WeaponStats _baseWeaponStats;
        private readonly WeaponStatsMultiplier _weaponStatsMultiplier;
        
        public WeaponStatsCalculator(WeaponStats baseWeaponStats, WeaponStatsMultiplier weaponStatsMultiplier)
        {
            _baseWeaponStats = baseWeaponStats;
            _weaponStatsMultiplier = weaponStatsMultiplier;
        }
        
        // Range is constant for weapon, so it doesn't need a multiplier.
        public float GetRange() => 
            _baseWeaponStats.Range;

        // Magazine capacity is constant for weapon, so it doesn't need a multiplier.
        public int GetMagazineCapacity() => 
            _baseWeaponStats.MagazineCapacity;
        
        public float GetReloadTime() => 
            _baseWeaponStats.ReloadTime * _weaponStatsMultiplier.Multiply(WeaponStatType.ReloadTime);
        
        /// <summary>
        /// Calculated spread is random between -spread and +spread from stats.
        /// </summary>
        /// <returns>Returns random value between -spread degree and +spread degree from weapon stats.</returns>
        public float GetSpreadDegree() => 
            Random.Range(-_baseWeaponStats.SpreadDegree * _weaponStatsMultiplier.Multiply(WeaponStatType.SpreadDegree), 
                _baseWeaponStats.SpreadDegree * _weaponStatsMultiplier.Multiply(WeaponStatType.SpreadDegree));
        
        public float GetDamageToDeal() => 
            _baseWeaponStats.DamageToDeal * _weaponStatsMultiplier.Multiply(WeaponStatType.DamageToDeal);
        
        public float GetSpeed() => 
            _baseWeaponStats.Speed * _weaponStatsMultiplier.Multiply(WeaponStatType.Speed);
        
        public float GetCooldownTime() => 
            _baseWeaponStats.CooldownTime * _weaponStatsMultiplier.Multiply(WeaponStatType.CooldownTime);
    }
}