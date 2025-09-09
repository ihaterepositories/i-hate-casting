using System;
using Models.Creatures.Base;
using Models.Creatures.Implementations.EnemyImplementation;
using Models.Items.Weapons.Bullets.Implementations.EnemyBulletImplementation;
using UnityEngine;

namespace Models.Creatures.Implementations.PlayerImplementation
{
    public class Player : Creature
    {
        public static event Action OnPlayerDeath;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<Enemy>(out var enemy))
                DoDamage(2);
            
            if (other.collider.TryGetComponent<EnemyBullet>(out var enemyBullet))
                DoDamage(enemyBullet.DamageToDeal);
        }
        
        public override void Kill()
        {
            OnPlayerDeath?.Invoke();
            Debug.Log("Player Died");
        }

        protected override void InstantiateDeathEffect()
        {
            // TODO: Implement player death effect
        }
    }
}