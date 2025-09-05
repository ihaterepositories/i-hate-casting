using System;
using Models.Creatures.Base.StatsHandling;
using Models.Creatures.Base.StatsHandling.ScriptableObjects;
using Models.Pooling;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Base
{
    public class Creature : PoolAbleMonoBehaviour
    {
        [SerializeField] protected CreatureStatsSo _creatureStatsSo;
        
        private CreatureStatsMultipliersProvider _creatureStatsMultipliersProvider;
        private CreatureStatsCalculator _statsCalculator;
        
        public CreatureStatsCalculator StatsCalculator => _statsCalculator;
        
        public event Action OnDeath;

        [Inject]
        private void Construct(CreatureStatsMultipliersProvider creatureStatsMultipliersProvider)
        {
            _creatureStatsMultipliersProvider = creatureStatsMultipliersProvider;
        }

        private void Awake()
        {
            var creatureStatsMultiplier = _creatureStatsMultipliersProvider.GetFor(_creatureStatsSo.CreatureType);
            _statsCalculator = new CreatureStatsCalculator(_creatureStatsSo, creatureStatsMultiplier);
        }

        public void Kill()
        {
            OnDeath?.Invoke();
            ReturnToPool();
        }
    }
}