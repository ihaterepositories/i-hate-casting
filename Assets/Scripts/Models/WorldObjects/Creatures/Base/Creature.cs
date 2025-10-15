using Models.WorldObjects.Base.Pooling;
using Models.WorldObjects.Creatures.Base.Living.Interfaces;
using Models.WorldObjects.Creatures.Base.Moving.Enums;
using Models.WorldObjects.Creatures.Base.Moving.Interfaces;
using Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Enums;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using Models.WorldObjects.Creatures.Base.StatsHandling.DataContainers;
using Models.WorldObjects.Creatures.Base.StatsHandling.Fabrics;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base
{
    /// <summary>
    /// Helper class that contains common logic for all creatures.
    /// </summary>
    public abstract class Creature : PoolableMonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        [Header("Settings")]
        [SerializeField] private MoveType _moveType;
        [SerializeField] private ObstaclesBypassType _obstaclesBypassType;
        [SerializeField] private CreatureStats _creatureStats;
        
        private CreatureStatsCalculator _statsCalculator;
        private CreatureStatsMultiplier _creatureStatsMultiplier;
        
        protected IMoveService _mover;
        protected IHealthService _health;
        
        protected Rigidbody2D Rigidbody2D => _rigidbody2D;
        protected ObstaclesBypassType ObstaclesBypassType => _obstaclesBypassType;
        protected MoveType MoveType => _moveType;
        
        public CreatureStatsCalculator StatsCalculator => _statsCalculator;
        public IHealthService Health => _health;

        protected void InitializeStatsHandling(CreatureStatsMultiplierFactory creatureStatsMultiplierFactory)
        {
            _creatureStatsMultiplier = creatureStatsMultiplierFactory.GetFor(_creatureStats.CreatureType);
            _statsCalculator = new CreatureStatsCalculator(_creatureStats, _creatureStatsMultiplier);
        }

        protected void InitializeStats(IHealthService healthService)
        {
            _health = healthService;
            _health.Refresh(_creatureStats.MaxHealth);
        }

        public override void OnTakenFromPool()
        {
            _health.Refresh(_statsCalculator.GetMaxHealth());
        }

        public void Damage(float value)
        {
            _health.ChangeBy(-value, _statsCalculator.GetMaxHealth());
        }
        
        public void Heal(float value)
        {
            _health.ChangeBy(value, _statsCalculator.GetMaxHealth());
        }
        
        public virtual void Kill()
        {
            ReturnToPool();
        }
    }
}