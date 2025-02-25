using System;

namespace Models.Weapon.Data
{
    [Serializable]
    public class WeaponStats
    {
        public int magazineCapacity;
        public float reloadTime;
        public float spread;
    }
}