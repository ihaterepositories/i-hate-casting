using System;
using Models.Creatures.Base.StatsHandling;
using Models.Creatures.Base.StatsHandling.ScriptableObjects;
using Pooling;
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
    }
}