using System;
using UnityEngine.Serialization;

namespace Models.Weapon.Bullets.Data
{
    [Serializable]
    public class BulletStats
    {
        public int damage;
        public float speed;
        public float lifeTime;
    }
}