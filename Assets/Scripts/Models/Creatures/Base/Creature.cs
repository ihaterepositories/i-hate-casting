using System;
using Models.Creatures.Base.StatsHandling;
using Models.Creatures.Base.StatsHandling.ScriptableObjects;
using Models.Pooling;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Base
{
    public abstract class Creature : PoolAbleMonoBehaviour
    {
        [SerializeField] protected CreatureStatsSo _creatureStatsSo;
        
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
            var creatureStatsMultiplier = _creatureStatsMultipliersProvider.GetFor(_creatureStatsSo.CreatureType);
            _statsCalculator = new CreatureStatsCalculator(_creatureStatsSo, creatureStatsMultiplier);
            
            Init();
        }

        public override void Init()
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
            InstantiateDeathEffect();
            OnDeath?.Invoke();
            ReturnToPool();
        }

        protected abstract void InstantiateDeathEffect();
    }
}