using Models.Creatures.Items.Implementations.Weapons.Base.StatsHandling.Enums;
using Models.Creatures.Items.Implementations.Weapons.Base.StatsHandling.ScriptableObjects;

namespace Models.Creatures.Items.Implementations.Weapons.Base.StatsHandling
{
    /// <summary>
    /// Util class, calculates weapon stats based on base stats and stats multiplying.
    /// </summary>
    public class WeaponStatsCalculator
    {
        private readonly WeaponStatsSo _baseWeaponStats;
        private readonly WeaponStatsMultiplier _weaponStatsMultiplier;
        
        public WeaponStatsCalculator(WeaponStatsSo baseWeaponStats, WeaponStatsMultiplier weaponStatsMultiplier)
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
            _baseWeaponStats.ReloadTime * _weaponStatsMultiplier.GetMultiplier(WeaponStatType.ReloadTime);
        
        public float GetSpread() => 
            _baseWeaponStats.Spread * _weaponStatsMultiplier.GetMultiplier(WeaponStatType.Spread);
        
        public float GetDamageToDeal() => 
            _baseWeaponStats.DamageToDeal * _weaponStatsMultiplier.GetMultiplier(WeaponStatType.DamageToDeal);
        
        public float GetSpeed() => 
            _baseWeaponStats.Speed * _weaponStatsMultiplier.GetMultiplier(WeaponStatType.Speed);
        
        public float GetCooldownTime() => 
            _baseWeaponStats.CooldownTime * _weaponStatsMultiplier.GetMultiplier(WeaponStatType.CooldownTime);
    }
}