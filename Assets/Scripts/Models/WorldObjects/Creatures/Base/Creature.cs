using System;
using Models.WorldObjects.Base.Pooling;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using Models.WorldObjects.Creatures.Base.StatsHandling.DataContainers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Models.WorldObjects.Creatures.Base
{
    public abstract class Creature : PoolableMonoBehaviour
    {
        [FormerlySerializedAs("_creatureStatsSo")] [SerializeField] protected CreatureStats _creatureStats;
        
        private CreatureStatsMultipliersProvider _creatureStatsMultipliersProvider;
        private CreatureStatsCalculator _statsCalculator;
        
        private float _health;
        
        public CreatureStatsCalculator StatsCalculator => _statsCalculator;
        public float Health => _health;
        
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
            ReturnToPool();
        }
    }
}