using Models.Creatures.Implementations.PlayerImplementation;
using Models.Creatures.Items.Implementations.Weapons.Bullets.Base;
using UnityEngine;

namespace Models.Creatures.Items.Implementations.Weapons.Bullets.Implementations.PlayerBulletImplementation
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