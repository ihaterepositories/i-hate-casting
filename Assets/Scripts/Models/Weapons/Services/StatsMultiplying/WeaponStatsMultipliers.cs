using Models.Weapons.Dtos;
using Models.Weapons.Services.StatsMultiplying.Interfaces;

namespace Models.Weapons.Services.StatsMultiplying
{
    public class WeaponStatsMultipliers : IWeaponStatsMultipliers
    {
        private float _reloadTimeMultiplier = 1f;
        private float _spreadDegreeMultiplier = 1f;
        private float _damageToDealMultiplier = 1f;
        private float _speedMultiplier = 1f;
        private float _cooldownTimeMultiplier = 1f;
        
        public float ModifyReloadTime(float baseReloadTime) => baseReloadTime * _reloadTimeMultiplier;
        public float ModifySpreadDegree(float baseSpread) => baseSpread * _spreadDegreeMultiplier;
        public float ModifyDamageToDeal(float baseDamage) => baseDamage * _damageToDealMultiplier;
        public float ModifySpeed(float baseSpeed) => baseSpeed * _speedMultiplier;
        public float ModifyCooldownTime(float baseCooldownTime) => baseCooldownTime * _cooldownTimeMultiplier;
        
        public void AddValuesToMultipliers(WeaponStats modifiers)
        {
            ChangeMultipliersValues(modifiers, 1);
        }

        public void SubtractValuesFromMultipliers(WeaponStats modifiers)
        {
            ChangeMultipliersValues(modifiers, -1);
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