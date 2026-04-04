using Models.Weapons.Dtos;
using Models.Weapons.Services.StatsScalers.Interfaces;

namespace Models.Weapons.Services.StatsScalers
{
    public class WeaponStatsScaler : IWeaponStatsScaler
    {
        private float _reloadTimeMultiplier = 1f;
        private float _spreadDegreeMultiplier = 1f;
        private float _damageToDealMultiplier = 1f;
        private float _speedMultiplier = 1f;
        private float _cooldownTimeMultiplier = 1f;
        
        public float ScaleReloadTime(float baseReloadTime) => baseReloadTime * _reloadTimeMultiplier;
        public float ScaleSpreadDegree(float baseSpread) => baseSpread * _spreadDegreeMultiplier;
        public float ScaleDamageToDeal(float baseDamage) => baseDamage * _damageToDealMultiplier;
        public float ScaleSpeed(float baseSpeed) => baseSpeed * _speedMultiplier;
        public float ScaleCooldownTime(float baseCooldownTime) => baseCooldownTime * _cooldownTimeMultiplier;
        
        public void AddMultipliers(WeaponStats valuesToAdd)
        {
            ChangeMultipliersValues(valuesToAdd, 1);
        }

        public void RemoveMultipliers(WeaponStats valuesToSubtract)
        {
            ChangeMultipliersValues(valuesToSubtract, -1);
        }

        private void ChangeMultipliersValues(WeaponStats modifiers, int operation = 1)
        {
            _reloadTimeMultiplier += operation * modifiers.ReloadTime;
            _spreadDegreeMultiplier += operation * modifiers.SpreadDegree;
            _damageToDealMultiplier += operation * modifiers.DamageToDeal;
            _speedMultiplier += operation * modifiers.Speed;
            _cooldownTimeMultiplier += operation * modifiers.CooldownTime;
        }
    }
}