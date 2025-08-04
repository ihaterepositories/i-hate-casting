using Models.Weapons.Base.Enums;
using UnityEngine;

namespace Models.Weapons.Base
{
    public class WeaponStatsMultiplier : MonoBehaviour
    {
        private float _reloadTimeMultiplier = 1f;
        private float _spreadMultiplier = 1f;
        private float _damageMultiplier = 1;
        private float _speedMultiplier = 1f;
        private float _cooldownTimeMultiplier = 1f;
        
        // ???
        //public float LifeTimeMultiplier = 1f;
        
        public float GetMultiplier(WeaponStatType type)
        {
            return type switch
            {
                WeaponStatType.ReloadTime => _reloadTimeMultiplier,
                WeaponStatType.Spread => _spreadMultiplier,
                WeaponStatType.Damage => _damageMultiplier,
                WeaponStatType.Speed => _speedMultiplier,
                WeaponStatType.CooldownTime => _cooldownTimeMultiplier,
                _ => 1f
            };
        }
        
        /// <summary>
        /// Modify one of weapon stats multipliers.
        /// </summary>
        /// <param name="type">Type of weapon stats multiplier to modify.</param>
        /// <param name="value">Number that will be added to current multiplier's value.</param>
        public void AddValueToMultiplier(WeaponStatType type, float value)
        {
            switch (type)
            {
                case WeaponStatType.ReloadTime: _reloadTimeMultiplier += value; break;
                case WeaponStatType.Spread: _spreadMultiplier += value; break;
                case WeaponStatType.Damage: _damageMultiplier += (int)value; break;
                case WeaponStatType.Speed: _speedMultiplier += value; break;
                case WeaponStatType.CooldownTime: _cooldownTimeMultiplier += value; break;
            }
        }
    }
}