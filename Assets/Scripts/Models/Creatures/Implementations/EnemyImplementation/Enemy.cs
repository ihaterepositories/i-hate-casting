using Models.Creatures.Base;
using Models.Creatures.Implementations.EnemyImplementation.Visuals.Pools;
using Models.Creatures.Implementations.PlayerImplementation;
using Models.Items.Weapons.Bullets.Implementations.PlayerBulletImplementation;
using UnityEngine;
using Zenject;


namespace Models.Creatures.Implementations.EnemyImplementation
{
    public class Enemy : Creature
    {
        private ExplosionEffectsPool _explosionEffectsPool;
        
        [Inject]
        private void Construct(ExplosionEffectsPool explosionEffectsPool)
        {
            _explosionEffectsPool = explosionEffectsPool;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<Player>(out var player))
                Kill();
            
            if (other.collider.TryGetComponent<PlayerBullet>(out var playerBullet))
                DoDamage(playerBullet.DamageToDeal);
        }

        protected override void InstantiateDeathEffect()
        {
            var explosion = _explosionEffectsPool.GetFreeObject();
            explosion.transform.position = transform.position;
            explosion.Play();
        }
    }
}