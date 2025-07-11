using Models.Weapons.Data.WeaponStatsMultipliers.Abstraction.Enums;
using UnityEngine;

namespace Models.Weapons.Data.WeaponStatsMultipliers.Abstraction
{
    public class WeaponStatsMultiplier : MonoBehaviour
    {
        protected float ReloadTimeMultiplier = 1f;
        protected float SpreadMultiplier = 1f;
        protected float DamageMultiplier = 1;
        protected float SpeedMultiplier = 1f;
        protected float CooldownTimeMultiplier = 1f;
        
        // ???
        //public float LifeTimeMultiplier = 1f;
        
        public float GetMultiplier(WeaponStatsType type)
        {
            return type switch
            {
                WeaponStatsType.ReloadTime => ReloadTimeMultiplier,
                WeaponStatsType.Spread => SpreadMultiplier,
                WeaponStatsType.Damage => DamageMultiplier,
                WeaponStatsType.Speed => SpeedMultiplier,
                WeaponStatsType.CooldownTime => CooldownTimeMultiplier,
                _ => 1f
            };
        }
        
        /// <summary>
        /// Modify one of weapon stats multipliers.
        /// </summary>
        /// <param name="type">Type of weapon stats multiplier to modify.</param>
        /// <param name="value">Number that will be added to current multiplier's value.</param>
        public void AddValueToMultiplier(WeaponStatsType type, float value)
        {
            switch (type)
            {
                case WeaponStatsType.ReloadTime: ReloadTimeMultiplier += value; break;
                case WeaponStatsType.Spread: SpreadMultiplier += value; break;
                case WeaponStatsType.Damage: DamageMultiplier += (int)value; break;
                case WeaponStatsType.Speed: SpeedMultiplier += value; break;
                case WeaponStatsType.CooldownTime: CooldownTimeMultiplier += value; break;
            }
        }
    }
}