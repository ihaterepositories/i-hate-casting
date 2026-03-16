using Models.Weapons.Dtos;

namespace Models.Weapons.Services.StatsMultiplying.Interfaces
{
    public interface IWeaponStatsMultipliers
    {
        public float ModifyReloadTime(float baseReloadTime);
        public float ModifySpreadDegree(float baseSpread);
        public float ModifyDamageToDeal(float baseDamage);
        public float ModifySpeed(float baseSpeed);
        public float ModifyCooldownTime(float baseCooldownTime);

        public void AddValuesToMultipliers(WeaponStats modifiers);
        public void SubtractValuesFromMultipliers(WeaponStats modifiers);
    }
}