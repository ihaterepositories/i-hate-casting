using Models.Items.Bullets.Base;
using Models.WorldObjects.Creatures.PlayerImpl;
using UnityEngine;

namespace Models.Items.Bullets.PlayerBulletImpl
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