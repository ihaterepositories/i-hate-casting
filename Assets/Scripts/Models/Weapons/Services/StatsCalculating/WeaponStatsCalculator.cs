using Models.Weapons.Dtos;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using Models.Weapons.Services.StatsModifying.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.StatsCalculating
{
    public class WeaponStatsCalculator : IWeaponStatsCalculator
    {
        private readonly WeaponStats _baseStats;
        private readonly IWeaponStatsModifier _statsModifier;
        
        public WeaponStatsCalculator(WeaponStats baseStats, IWeaponStatsModifier statsModifier)
        {
            _baseStats = baseStats;
            _statsModifier = statsModifier;
        }
        
        // Range is constant for weapon, it does not modify.
        public float CalculateRange() => _baseStats.Range;

        // Magazine capacity is constant for weapon, it does not modify.
        public int CalculateMagazineCapacity() => _baseStats.MagazineCapacity;

        public float CalculateReloadTime() => _statsModifier.ModifyReloadTime(_baseStats.ReloadTime);
        
        public float CalculateSpreadDegree() => Random.Range(
            -_statsModifier.ModifySpreadDegree(_baseStats.SpreadDegree), 
            _statsModifier.ModifySpreadDegree(_baseStats.SpreadDegree));
        
        public float CalculateDamageToDeal() => _statsModifier.ModifyDamageToDeal(_baseStats.DamageToDeal);

        public float CalculateSpeed() => _statsModifier.ModifySpeed(_baseStats.Speed);

        public float CalculateCooldownTime() => _statsModifier.ModifyCooldownTime(_baseStats.CooldownTime);
    }
}