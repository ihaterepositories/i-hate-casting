using Models.Items.Weapons.Bullets.PlayerBulletImpl;
using Models.WorldObjects.Creatures.Base;
using Models.WorldObjects.Creatures.PlayerImpl;
using UnityEngine;

namespace Models.WorldObjects.Creatures.EnemyImpl
{
    public class Enemy : Creature
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<Player>(out var player))
                Kill();
            
            if (other.collider.TryGetComponent<PlayerBullet>(out var playerBullet))
                DoDamage(playerBullet.DamageToDeal);
        }
    }
}