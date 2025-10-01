using Models.Creatures.PlayerImpl;
using Models.Items.Weapons.Bullets.Base;
using UnityEngine;

namespace Models.Items.Weapons.Bullets.PlayerBulletImpl
{
    public class PlayerBullet : Bullet
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<PlayerBullet>(out _)) return;
            if (other.collider.TryGetComponent<Player>(out _)) return;
            
            ReturnToPool();
        }
    }
}