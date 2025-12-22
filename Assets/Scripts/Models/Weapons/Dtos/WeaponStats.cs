using System;
using UnityEngine;

namespace Models.Weapons.Dtos
{
    [Serializable]
    public class WeaponStats
    {
        [Header("Short range: 1-2, Medium range: 3-5, Long range: 6+")]
        public float Range;
        public int MagazineCapacity;
        [Header("2 seconds is minimum")]
        public float ReloadTime;
        public float SpreadDegree;
        public float DamageToDeal;
        public float Speed;
        public float CooldownTime;
    }
}