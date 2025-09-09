using System;
using Models.Creatures.Implementations.EnemyImplementation;
using Models.Items.Weapons.Bullets.Base;
using UnityEngine;

namespace Models.Items.Weapons.Bullets.Implementations.EnemyBulletImplementation
{
    public class EnemyBullet : Bullet
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<EnemyBullet>(out _)) return;
            if (other.collider.TryGetComponent<Enemy>(out _)) return;
            
            ReturnToPool();
        }
    }
}