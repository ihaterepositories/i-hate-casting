using System;
using Core.GameControl;
using Models.Items.Weapons.Bullets.EnemyBulletImpl;
using Models.WorldObjects.Creatures.Base;
using Models.WorldObjects.Creatures.Base.Living.Interfaces;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Enums;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Fabrics;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Interfaces;
using Models.WorldObjects.Creatures.Base.Moving.Fabrics;
using Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Enums;
using Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Fabrics;
using Models.WorldObjects.Creatures.Base.StatsHandling.Fabrics;
using Models.WorldObjects.Creatures.EnemyImpl;
using UnityEngine;
using Zenject;

namespace Models.WorldObjects.Creatures.PlayerImpl
{
    public class Player : Creature
    {
        [SerializeField] private MoveBoostType _moveBoostType;
        [SerializeField] private bool _enableMoveBoost;
        
        private IMoveBoostService _moveBoostService;
        
        public IMoveBoostService MoveBoostService => _moveBoostService;
        
        public static event Action OnPlayerDeath;

        [Inject]
        private void Construct(
            CreatureStatsMultiplierFactory creatureStatsMultiplierFactory,
            IHealthService healthService,
            ObstaclesBypassersFabric obstaclesBypassersFabric,
            MoversFabric moversFabric,
            MoveBoostersFabric moveBoostersFabric)
        {
            InitializeStatsHandling(creatureStatsMultiplierFactory);
            InitializeStats(healthService);
            
            _mover = moversFabric.Create(MoveType, Rigidbody2D, transform, StatsCalculator);
            
            if (ObstaclesBypassType != ObstaclesBypassType.None)
            {
                var obstaclesBypasser = obstaclesBypassersFabric.Create(ObstaclesBypassType, transform);
                _mover.AssignObstaclesBypasser(obstaclesBypasser);
            }
            
            _moveBoostService = moveBoostersFabric.Create(_moveBoostType, Rigidbody2D, StatsCalculator);
        }

        private void Update()
        {
            if (GamePauser.IsGamePaused) return;
            
            _moveBoostService.HandleTimings();
        }

        private void FixedUpdate()
        {
            if (GamePauser.IsGamePaused) return;
            
            _mover.Move();
            
            if (_enableMoveBoost)
                _moveBoostService.ActivateBooster();
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
            if (other.collider.TryGetComponent<Enemy>(out var enemy))
                Damage(2);
            
            if (other.collider.TryGetComponent<EnemyBullet>(out var enemyBullet))
                Damage(enemyBullet.DamageToDeal);
        }
        
        public override void Kill()
        {
            OnPlayerDeath?.Invoke();
            Debug.Log("Player Died");
        }
    }
}