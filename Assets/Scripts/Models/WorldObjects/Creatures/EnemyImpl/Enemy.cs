using Core.GameControl;
using Models.Items.Weapons.Bullets.PlayerBulletImpl;
using Models.WorldObjects.Creatures.Base;
using Models.WorldObjects.Creatures.Base.Living.Interfaces;
using Models.WorldObjects.Creatures.Base.Moving.Fabrics;
using Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Enums;
using Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Fabrics;
using Models.WorldObjects.Creatures.Base.StatsHandling.Fabrics;
using Models.WorldObjects.Creatures.PlayerImpl;
using UnityEngine;
using Zenject;

namespace Models.WorldObjects.Creatures.EnemyImpl
{
    public class Enemy : Creature
    {
        
        
        [Inject]
        private void Construct(
            CreatureStatsMultiplierFactory creatureStatsMultiplierFactory,
            IHealthService healthService,
            ObstaclesBypassersFabric obstaclesBypassersFabric,
            MoversFabric moversFabric)
        {
            InitializeStatsHandling(creatureStatsMultiplierFactory);
            InitializeStats(healthService);
            
            _mover = moversFabric.Create(MoveType, Rigidbody2D, transform, StatsCalculator);

            if (ObstaclesBypassType != ObstaclesBypassType.None)
            {
                var obstaclesBypasser = obstaclesBypassersFabric.Create(ObstaclesBypassType, transform);
                _mover.AssignObstaclesBypasser(obstaclesBypasser);
            }
        }
        
        private void FixedUpdate()
        {
            if (GamePauser.IsGamePaused) return;
            
            _mover.Move();
        }
        
        private void OnEnable()
        {
            _health.OnHealthGone += Kill;
        }
        
        private void OnDisable()
        {
            _health.OnHealthGone -= Kill;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<Player>(out var player))
                Kill();
            
            if (other.collider.TryGetComponent<PlayerBullet>(out var playerBullet))
                Damage(playerBullet.DamageToDeal);
        }
    }
}