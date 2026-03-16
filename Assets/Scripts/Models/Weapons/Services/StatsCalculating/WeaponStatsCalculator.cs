using Models.Weapons.Dtos;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using Models.Weapons.Services.StatsMultiplying.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.StatsCalculating
{
    public class WeaponStatsCalculator : IWeaponStatsCalculator
    {
        private readonly WeaponStats _baseStats;
        private readonly IWeaponStatsMultipliers _statsMultipliers;
        
        public WeaponStatsCalculator(WeaponStats baseStats, IWeaponStatsMultipliers statsMultipliers)
        {
            _baseStats = baseStats;
            _statsMultipliers = statsMultipliers;
        }
        
        // Range is constant for weapon, it does not modify.
        public float CalculateRange() => _baseStats.Range;

        // Magazine capacity is constant for weapon, it does not modify.
        public int CalculateMagazineCapacity() => _baseStats.MagazineCapacity;

        public float CalculateReloadTime() => _statsMultipliers.ModifyReloadTime(_baseStats.ReloadTime);
        
        public float CalculateSpreadDegree() => Random.Range(
            -_statsMultipliers.ModifySpreadDegree(_baseStats.SpreadDegree), 
            _statsMultipliers.ModifySpreadDegree(_baseStats.SpreadDegree));
        
        public float CalculateDamageToDeal() => _statsMultipliers.ModifyDamageToDeal(_baseStats.DamageToDeal);

        public float CalculateSpeed() => _statsMultipliers.ModifySpeed(_baseStats.Speed);

        public float CalculateCooldownTime() => _statsMultipliers.ModifyCooldownTime(_baseStats.CooldownTime);
    }
}