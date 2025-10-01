using System;
using Models.Creatures.Base.StatsHandling;
using Models.Creatures.Base.StatsHandling.DataContainers;
using Models.Pooling;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Models.Creatures.Base
{
    public abstract class Creature : PoolAbleMonoBehaviour
    {
        [FormerlySerializedAs("_creatureStatsSo")] [SerializeField] protected CreatureStats _creatureStats;
        
        private CreatureStatsMultipliersProvider _creatureStatsMultipliersProvider;
        private CreatureStatsCalculator _statsCalculator;
        
        private float _health;
        
        public CreatureStatsCalculator StatsCalculator => _statsCalculator;
        public float Health => _health;
        
        public event Action OnDeath;
        public event Action OnDamaged; 

        [Inject]
        private void Construct(CreatureStatsMultipliersProvider creatureStatsMultipliersProvider)
        {
            _creatureStatsMultipliersProvider = creatureStatsMultipliersProvider;
        }

        private void Awake()
        {
            var creatureStatsMultiplier = _creatureStatsMultipliersProvider.GetFor(_creatureStats.CreatureType);
            _statsCalculator = new CreatureStatsCalculator(_creatureStats, creatureStatsMultiplier);
            
            OnTakenFromPool();
        }

        public override void OnTakenFromPool()
        {
            _health = _statsCalculator.GetMaxHealth();
        }

        public void DoDamage(float damage)
        {
            _health -= damage;
            OnDamaged?.Invoke();
            if (_health <= 0)
                Kill();
        }
        
        public virtual void Kill()
        {
            OnDeath?.Invoke();
            ReturnToPool();
        }
    }
}