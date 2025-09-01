using Models.Creatures.Base.StatsHandling;
using Models.Creatures.Base.StatsHandling.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Base
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] protected CreatureStatsSo _creatureStatsSo;
        
        private CreatureStatsMultipliersProvider _creatureStatsMultipliersProvider;
        private CreatureStatsCalculator _creatureStatsCalculator;
        
        public CreatureStatsCalculator CreatureStatsCalculator => _creatureStatsCalculator;

        [Inject]
        private void Construct(CreatureStatsMultipliersProvider creatureStatsMultipliersProvider)
        {
            _creatureStatsMultipliersProvider = creatureStatsMultipliersProvider;
        }

        private void Awake()
        {
            var creatureStatsMultiplier = _creatureStatsMultipliersProvider.GetFor(_creatureStatsSo.CreatureType);
            _creatureStatsCalculator = new CreatureStatsCalculator(_creatureStatsSo, creatureStatsMultiplier);
        }
    }
}