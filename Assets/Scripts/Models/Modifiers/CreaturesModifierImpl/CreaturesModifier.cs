using Models.Creatures.Enums;
using Models.Creatures.Services.StatsCalculating.StatsModifying.Interfaces;
using Models.Creatures.Services.StatsCalculating.StatsModifying.Providers;
using Models.Modifiers.Base;
using Models.Modifiers.CreaturesModifierImpl.DataContainers;
using UnityEngine;
using Zenject;

namespace Models.Modifiers.CreaturesModifierImpl
{
    public class CreaturesModifier : Modifier
    {
        [SerializeField] private CreaturesModifierStats _creaturesModifierStats;

        private CreatureStatsModifiersProvider _creatureStatsModifiersProvider;
        private (ICreatureStatsModifier multiplier, CreatureModifyingValues stats)[] _creaturesModifyingData;

        [Inject]
        private void Construct(CreatureStatsModifiersProvider creatureStatsModifiersProvider)
        {
            _creatureStatsModifiersProvider = creatureStatsModifiersProvider;
        }

        private void Awake()
        {
            _creaturesModifyingData = new[]
            {
                (_creatureStatsModifiersProvider.GetFor(CreatureType.Player), _creaturesModifierStats.PlayerModifyingValues),
                (_creatureStatsModifiersProvider.GetFor(CreatureType.Enemy), _creaturesModifierStats.DefaultEnemiesModifyingValues),
            };
        }

        protected override void ActivateModifier()
        {
            foreach (var (multiplier, stats) in _creaturesModifyingData)
            {
                multiplier.AddValuesToMultipliers(stats);
            }
        }

        protected override void DeactivateModifier()
        {
            foreach (var (multiplier, stats) in _creaturesModifyingData)
            {
                multiplier.SubtractValuesFromMultipliers(stats);
            }
        }
    }
}