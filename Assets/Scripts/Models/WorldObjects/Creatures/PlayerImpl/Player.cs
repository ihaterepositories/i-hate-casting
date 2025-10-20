using System;
using Core.GameControl;
using Models.Items.Bullets.EnemyBulletImpl;
using Models.WorldObjects.Creatures.Base;
using Models.WorldObjects.Creatures.Base.Living.Factories;
using Models.WorldObjects.Creatures.Base.Living.Interfaces;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Enums;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Factories;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Interfaces;
using Models.WorldObjects.Creatures.Base.Moving.Factories;
using Models.WorldObjects.Creatures.Base.ObstaclesBypassing.Enums;
using Models.WorldObjects.Creatures.Base.ObstaclesBypassing.Factories;
using Models.WorldObjects.Creatures.Base.StatsHandling.Providers;
using Models.WorldObjects.Creatures.EnemyImpl;
using UnityEngine;
using Zenject;

namespace Models.WorldObjects.Creatures.PlayerImpl
{
    public class Player : Creature
    {
        [Inject]
        private void Construct(
            CreatureStatsMultipliersProvider creatureStatsMultipliersProvider,
            HealthServicesFactory healthServicesFactory,
            MoversFactory moversFactory,
            ObstaclesBypassersFactory obstaclesBypassersFactory,
            MoveBoostersFactory moveBoostersFactory)
        {
            InitializeServices(
                creatureStatsMultipliersProvider,
                healthServicesFactory,
                moversFactory, 
                obstaclesBypassersFactory, 
                moveBoostersFactory);
        }
        
        private void OnEnable()
        {
            Health.OnHealthGone += Kill;
        }
        
        private void OnDisable()
        {
            Health.OnHealthGone -= Kill;
        }

        private void Update()
        {
            if (GamePauser.IsGamePaused) return;
            
            if (MoveBoostType != MoveBoostType.None)
                MoveBooster.HandleTimings();
        }

        private void FixedUpdate()
        {
            if (GamePauser.IsGamePaused) return;
            
            Mover.EnableMove();
            
            if (MoveBoostType != MoveBoostType.None)
                MoveBooster.EnableBoost();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<Enemy>(out var enemy))
                Damage(2);
            
            if (other.collider.TryGetComponent<EnemyBullet>(out var enemyBullet))
                Damage(enemyBullet.DamageToDeal);
        }

        protected override void Kill()
        {
            Debug.Log("Player has died.");
        }
    }
}