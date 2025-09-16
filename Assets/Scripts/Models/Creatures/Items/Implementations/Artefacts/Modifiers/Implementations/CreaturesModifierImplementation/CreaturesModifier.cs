using Models.Creatures.Base.Enums;
using Models.Creatures.Base.StatsHandling;
using Models.Creatures.Items.Implementations.Artefacts.Modifiers.Base;
using Models.Creatures.Items.Implementations.Artefacts.Modifiers.Implementations.CreaturesModifierImplementation.ScriptableObjects;
using Models.Creatures.Items.Implementations.Artefacts.Modifiers.Implementations.CreaturesModifierImplementation.ScriptableObjects.SerializingDataContainers;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Items.Implementations.Artefacts.Modifiers.Implementations.CreaturesModifierImplementation
{
    public class CreaturesModifier : Modifier
    {
        [SerializeField] private CreaturesModifierStatsSo _creaturesModifierStats;

        private CreatureStatsMultipliersProvider _creatureStatsMultipliersProvider;
        private (CreatureStatsMultiplier multiplier, CreatureModifyingValues stats)[] _creaturesModifyingData;

        [Inject]
        private void Construct(CreatureStatsMultipliersProvider creatureStatsMultipliersProvider)
        {
            _creatureStatsMultipliersProvider = creatureStatsMultipliersProvider;
        }

        private void Awake()
        {
            _creaturesModifyingData = new[]
            {
                (_creatureStatsMultipliersProvider.GetFor(CreatureType.Player), _creaturesModifierStats.PlayerModifyingValues),
                (_creatureStatsMultipliersProvider.GetFor(CreatureType.Enemy), _creaturesModifierStats.DefaultEnemiesModifyingValues),
                (_creatureStatsMultipliersProvider.GetFor(CreatureType.Boss), _creaturesModifierStats.BossModifyingValues)
            };
        }

        protected override void ActivateModifier()
        {
            foreach (var (multiplier, stats) in _creaturesModifyingData)
            {
                multiplier.AddValuesToMultiplier(stats);
            }
        }

        protected override void DeactivateModifier()
        {
            foreach (var (multiplier, stats) in _creaturesModifyingData)
            {
                multiplier.SubtractValuesFromMultiplier(stats);
            }
        }
    }
}