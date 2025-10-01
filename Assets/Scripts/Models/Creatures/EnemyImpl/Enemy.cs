using Models.Creatures.Base;
using Models.Creatures.PlayerImpl;
using Models.Items.Weapons.Bullets.PlayerBulletImpl;
using UnityEngine;

namespace Models.Creatures.EnemyImpl
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