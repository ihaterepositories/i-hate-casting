using Models.Items.Weapons.Bullets.Base;
using Models.WorldObjects.Creatures.EnemyImpl;
using UnityEngine;

namespace Models.Items.Weapons.Bullets.EnemyBulletImpl
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