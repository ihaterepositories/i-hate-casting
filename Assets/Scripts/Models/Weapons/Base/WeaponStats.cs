using System;
using Models.Weapons.Data.WeaponStatsMultipliers;
using Models.Weapons.Data.WeaponStatsMultipliers.Abstraction;
using Models.Weapons.Data.WeaponStatsMultipliers.Abstraction.Enums;
using Models.Weapons.Enums;
using UnityEngine;
using Zenject;

namespace Models.Weapons.Data
{
    [Serializable]
    public class WeaponStats
    {
        public WeaponType weaponType;

        [Header("Short range: 2-5, Medium range: 5-8, Long range: 9+")]
        [SerializeField] private float range;
        [SerializeField] private int magazineCapacity;
        [SerializeField] private float reloadTime;
        [SerializeField] private float spread;
        [SerializeField] private float damage;
        [SerializeField] private float speed;
        [SerializeField] private float cooldownTime;

        private WeaponStatsMultiplier _statsMultiplier;

        public void SetStatsMultiplier(WeaponStatsMultiplier weaponStatsMultiplier)
        {
            if (weaponStatsMultiplier != null)
            {
                Debug.Log("Weapon stats multiplier assigned.");
            }
            _statsMultiplier = weaponStatsMultiplier;
        }

        public float GetRange() => range;

        public int GetMagazineCapacity() => magazineCapacity;
        
        public float GetReloadTime() => reloadTime * _statsMultiplier.GetMultiplier(WeaponStatsType.ReloadTime);
        
        public float GetSpread() => spread * _statsMultiplier.GetMultiplier(WeaponStatsType.Spread);
        
        public float GetDamage() => damage * _statsMultiplier.GetMultiplier(WeaponStatsType.Damage);
        
        public float GetSpeed() => speed * _statsMultiplier.GetMultiplier(WeaponStatsType.Speed);
        
        public float GetCooldownTime() => cooldownTime * _statsMultiplier.GetMultiplier(WeaponStatsType.CooldownTime);
    }
}