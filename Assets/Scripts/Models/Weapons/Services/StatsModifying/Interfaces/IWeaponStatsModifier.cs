using Models.Modifiers.WeaponsModifierImpl.DataContainers;

namespace Models.Weapons.Services.StatsModifying.Interfaces
{
    public interface IWeaponStatsModifier
    {
        public float ModifyReloadTime(float baseReloadTime);
        public float ModifySpreadDegree(float baseSpread);
        public float ModifyDamageToDeal(float baseDamage);
        public float ModifySpeed(float baseSpeed);
        public float ModifyCooldownTime(float baseCooldownTime);

        public void AddValuesToMultipliers(WeaponModifyingValues modifyingValuesToModify);
        public void SubtractValuesFromMultipliers(WeaponModifyingValues modifyingValuesToModify);
    }
}