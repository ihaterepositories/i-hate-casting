using System;
using Core.GameControl;
using Models.Items.Bullets.PlayerBulletImpl;
using Models.WorldObjects.Creatures.Base;
using Models.WorldObjects.Creatures.Base.Living.Factories;
using Models.WorldObjects.Creatures.Base.Living.Interfaces;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Enums;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Factories;
using Models.WorldObjects.Creatures.Base.Moving.Factories;
using Models.WorldObjects.Creatures.Base.ObstaclesBypassing.Enums;
using Models.WorldObjects.Creatures.Base.ObstaclesBypassing.Factories;
using Models.WorldObjects.Creatures.Base.StatsHandling.Providers;
using Models.WorldObjects.Creatures.PlayerImpl;
using UnityEngine;
using Zenject;

namespace Models.WorldObjects.Creatures.EnemyImpl
{
    public class Enemy : Creature
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
            if (other.collider.TryGetComponent<Player>(out var player))
                Kill();
            
            if (other.collider.TryGetComponent<PlayerBullet>(out var playerBullet))
                Damage(playerBullet.DamageToDeal);
        }
    }
}