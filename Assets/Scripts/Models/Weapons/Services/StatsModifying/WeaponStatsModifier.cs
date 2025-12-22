using Models.Modifiers.WeaponsModifierImpl.DataContainers;
using Models.Weapons.Services.StatsModifying.Interfaces;

namespace Models.Weapons.Services.StatsModifying
{
    public class WeaponStatsModifier : IWeaponStatsModifier
    {
        private float _reloadTimeMultiplier = 1f;
        private float _spreadMultiplier = 1f;
        private float _damageToDealMultiplier = 1f;
        private float _speedMultiplier = 1f;
        private float _cooldownTimeMultiplier = 1f;
        
        public float ModifyReloadTime(float baseReloadTime) => baseReloadTime * _reloadTimeMultiplier;
        public float ModifySpreadDegree(float baseSpread) => baseSpread * _spreadMultiplier;
        public float ModifyDamageToDeal(float baseDamage) => baseDamage * _damageToDealMultiplier;
        public float ModifySpeed(float baseSpeed) => baseSpeed * _speedMultiplier;
        public float ModifyCooldownTime(float baseCooldownTime) => baseCooldownTime * _cooldownTimeMultiplier;
        
        public void AddValuesToMultipliers(WeaponModifyingValues modifyingValuesToModify)
        {
            ChangeMultipliersValues(modifyingValuesToModify, 1);
        }

        public void SubtractValuesFromMultipliers(WeaponModifyingValues modifyingValuesToModify)
        {
            ChangeMultipliersValues(modifyingValuesToModify, -1);
        }

        private void ChangeMultipliersValues(WeaponModifyingValues modifyingValuesToModify, int operation = 1)
        {
            _reloadTimeMultiplier += operation * modifyingValuesToModify.ReloadTimeModifier;
            _spreadMultiplier += operation * modifyingValuesToModify.SpreadModifier;
            _damageToDealMultiplier += operation * modifyingValuesToModify.DamageToDealModifier;
            _speedMultiplier += operation * modifyingValuesToModify.SpeedModifier;
            _cooldownTimeMultiplier += operation * modifyingValuesToModify.CooldownTimeModifier;
        }
    }
}