using System;
using Models.Creatures.Base;
using Models.Creatures.EnemyImpl;
using Models.Items.Weapons.Bullets.EnemyBulletImpl;
using UnityEngine;

namespace Models.Creatures.PlayerImpl
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
    }
}