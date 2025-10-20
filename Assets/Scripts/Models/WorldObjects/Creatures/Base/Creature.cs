using Models.WorldObjects.Base.Pooling;
using Models.WorldObjects.Creatures.Base.Living.Enums;
using Models.WorldObjects.Creatures.Base.Living.Factories;
using Models.WorldObjects.Creatures.Base.Living.Interfaces;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Enums;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Factories;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Interfaces;
using Models.WorldObjects.Creatures.Base.Moving.Enums;
using Models.WorldObjects.Creatures.Base.Moving.Factories;
using Models.WorldObjects.Creatures.Base.Moving.Interfaces;
using Models.WorldObjects.Creatures.Base.ObstaclesBypassing.Enums;
using Models.WorldObjects.Creatures.Base.ObstaclesBypassing.Factories;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using Models.WorldObjects.Creatures.Base.StatsHandling.DataContainers;
using Models.WorldObjects.Creatures.Base.StatsHandling.Providers;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base
{
    /// <summary>
    /// Helper class that contains common logic for all creatures.
    /// </summary>
    public class Creature : PoolableMonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        [Header("Settings")]
        [SerializeField] private HealthType _healthType;
        [SerializeField] private MoveType _moveType;
        [SerializeField] private ObstaclesBypassType _obstaclesBypassType;
        [SerializeField] private MoveBoostType _moveBoostType;
        [SerializeField] private CreatureStats _creatureStats;
        
        private CreatureStatsCalculator _statsCalculator;
        
        private IHealthService _health;
        private IMoveService _mover;
        private IMoveBoostService _moveBooster;
        
        protected MoveBoostType MoveBoostType => _moveBoostType;
        
        public IHealthService Health => _health;
        public IMoveService Mover => _mover;
        public IMoveBoostService MoveBooster => _moveBooster;

        protected void InitializeServices(
            CreatureStatsMultipliersProvider creatureStatsMultipliersProvider,
            HealthServicesFactory healthServicesFactory,
            MoversFactory moversFactory,
            ObstaclesBypassersFactory obstaclesBypassersFactory,
            MoveBoostersFactory moveBoostersFactory)
        {
            var creatureStatsMultiplier = creatureStatsMultipliersProvider.GetFor(_creatureStats.CreatureType);
            _statsCalculator = new CreatureStatsCalculator(_creatureStats, creatureStatsMultiplier);
            
            _health = healthServicesFactory.Create(_healthType, _statsCalculator);
            _health.Refresh();
            
            _mover = moversFactory.Create(_moveType, _statsCalculator, _rigidbody2D, transform);
            
            if (_obstaclesBypassType != ObstaclesBypassType.None)
            {
                var obstaclesBypasser = obstaclesBypassersFactory.Create(_obstaclesBypassType, transform);
                _mover.AssignObstaclesBypasser(obstaclesBypasser);
            }
            
            if (_moveBoostType != MoveBoostType.None)
                _moveBooster = moveBoostersFactory.Create(_moveBoostType, _rigidbody2D, _statsCalculator);
        }

        public override void OnTakenFromPool()
        {
            _health.Refresh();
        }

        public void Damage(float value)
        {
            _health.ChangeBy(-value);
        }
        
        public void Heal(float value)
        {
            _health.ChangeBy(value);
        }
        
        protected virtual void Kill()
        {
            ReturnToPool();
        }
    }
}