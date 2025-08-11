using Models.Items.Weapons.Base.Enums;
using UnityEngine;

namespace Models.Items.Weapons.Base.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponStats", menuName = "ScriptableObjects/WeaponStats")]
    public class WeaponStatsSo : ScriptableObject
    {
        [Header("Used to assign stats multiplier.")]
        public WeaponType WeaponType;

        [Header("Short range: 1-2, Medium range: 3-5, Long range: 6+")]
        [SerializeField] private float _range;
        [SerializeField] private int _magazineCapacity;
        [Header("2 seconds is minimum")]
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _spread;
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _cooldownTime;

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
        public float GetRange() => _range;

        public int GetMagazineCapacity() => _magazineCapacity;
        
        public float GetReloadTime() => _reloadTime * _statsMultiplier.GetMultiplier(WeaponStatType.ReloadTime);
        
        public float GetSpread() => _spread * _statsMultiplier.GetMultiplier(WeaponStatType.Spread);
        
        public float GetDamage() => _damage * _statsMultiplier.GetMultiplier(WeaponStatType.Damage);
        
        public float GetSpeed() => _speed * _statsMultiplier.GetMultiplier(WeaponStatType.Speed);
        
        public float GetCooldownTime() => _cooldownTime * _statsMultiplier.GetMultiplier(WeaponStatType.CooldownTime);
    }
}