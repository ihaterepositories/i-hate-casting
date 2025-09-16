using Models.Creatures.Base;
using Models.Creatures.Implementations.EnemyImplementation.Visuals.Pools;
using Models.Creatures.Implementations.PlayerImplementation;
using Models.Creatures.Items.Implementations.Weapons.Bullets.Implementations.PlayerBulletImplementation;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Implementations.EnemyImplementation
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