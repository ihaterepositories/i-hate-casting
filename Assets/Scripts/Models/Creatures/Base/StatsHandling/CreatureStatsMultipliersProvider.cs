using Models.Creatures.Base.Enums;
using UnityEngine;

namespace Models.Creatures.Base.StatsHandling
{
    public class CreatureStatsMultipliersProvider : MonoBehaviour
    {
        [SerializeField] private CreatureStatsMultiplier _playerStatsMultiplier;
        [SerializeField] private CreatureStatsMultiplier _enemiesStatsMultiplier;
        [SerializeField] private CreatureStatsMultiplier _bossesStatsMultiplier;
        
        public CreatureStatsMultiplier GetFor(CreatureType creatureType)
        {
            return creatureType switch
            {
                CreatureType.Player => _playerStatsMultiplier,
                CreatureType.DefaultEnemy => _enemiesStatsMultiplier,
                CreatureType.Boss => _bossesStatsMultiplier,
                _ => null
            };
        }
    }
}