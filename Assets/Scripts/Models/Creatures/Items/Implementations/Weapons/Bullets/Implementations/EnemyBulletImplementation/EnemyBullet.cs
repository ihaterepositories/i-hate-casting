using Models.Creatures.Implementations.EnemyImplementation;
using Models.Creatures.Items.Implementations.Weapons.Bullets.Base;
using UnityEngine;

namespace Models.Creatures.Items.Implementations.Weapons.Bullets.Implementations.EnemyBulletImplementation
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