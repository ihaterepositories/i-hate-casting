using Models.Items.Weapons.Base.Enums;
using UnityEngine;

namespace Models.Items.Weapons.Base.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponStats", menuName = "ScriptableObjects/WeaponStats")]
    public class WeaponStatsSO : ScriptableObject
    {
        [Header("Used to assign stats multiplier.")]
        public WeaponType weaponType;

        [Header("Short range: 1-2, Medium range: 3-5, Long range: 6+")]
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
            Debug.Log("Assigned: " + _statsMultiplier.gameObject.name);
        }

        // Range is constant for all weapons, so it doesn't need a multiplier.
        public float GetRange() => range;

        public int GetMagazineCapacity() => magazineCapacity;
        
        public float GetReloadTime() => reloadTime * _statsMultiplier.GetMultiplier(WeaponStatType.ReloadTime);
        
        public float GetSpread() => spread * _statsMultiplier.GetMultiplier(WeaponStatType.Spread);
        
        public float GetDamage() => damage * _statsMultiplier.GetMultiplier(WeaponStatType.Damage);
        
        public float GetSpeed() => speed * _statsMultiplier.GetMultiplier(WeaponStatType.Speed);
        
        public float GetCooldownTime() => cooldownTime * _statsMultiplier.GetMultiplier(WeaponStatType.CooldownTime);
    }
}