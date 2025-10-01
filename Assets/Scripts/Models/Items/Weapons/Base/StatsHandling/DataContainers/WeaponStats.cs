using System;
using Models.Items.Weapons.Base.Enums;
using UnityEngine;

namespace Models.Items.Weapons.Base.StatsHandling.DataContainers
{
    [Serializable]
    public class WeaponStats
    {
        [Header("Used to assign stats multiplier.")]
        public WeaponType WeaponType;
        [Header("Short range: 1-2, Medium range: 3-5, Long range: 6+")]
        public float Range;
        public int MagazineCapacity;
        [Header("2 seconds is minimum")]
        public float ReloadTime;
        public float Spread;
        public float DamageToDeal;
        public float Speed;
        public float CooldownTime;
    }
}