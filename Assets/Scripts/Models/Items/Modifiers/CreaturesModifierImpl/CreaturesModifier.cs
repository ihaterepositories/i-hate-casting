using Models.Items.Modifiers.Base;
using Models.Items.Modifiers.CreaturesModifierImpl.DataContainers;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using Models.WorldObjects.Creatures.Base.StatsHandling.Enums;
using Models.WorldObjects.Creatures.Base.StatsHandling.Fabrics;
using UnityEngine;
using Zenject;

namespace Models.Items.Modifiers.CreaturesModifierImpl
{
    public class CreaturesModifier : Modifier
    {
        [SerializeField] private CreaturesModifierStats _creaturesModifierStats;

        private CreatureStatsMultiplierFactory _creatureStatsMultiplierFactory;
        private (CreatureStatsMultiplier multiplier, CreatureModifyingValues stats)[] _creaturesModifyingData;

        [Inject]
        private void Construct(CreatureStatsMultiplierFactory creatureStatsMultiplierFactory)
        {
            _creatureStatsMultiplierFactory = creatureStatsMultiplierFactory;
        }

        private void Awake()
        {
            _creaturesModifyingData = new[]
            {
                (_creatureStatsMultiplierFactory.GetFor(CreatureType.Player), _creaturesModifierStats.PlayerModifyingValues),
                (_creatureStatsMultiplierFactory.GetFor(CreatureType.Enemy), _creaturesModifierStats.DefaultEnemiesModifyingValues),
                (_creatureStatsMultiplierFactory.GetFor(CreatureType.Boss), _creaturesModifierStats.BossModifyingValues)
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