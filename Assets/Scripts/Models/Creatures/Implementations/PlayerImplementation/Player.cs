using Models.Creatures.Base;
using Models.Creatures.Implementations.PlayerImplementation.StatsMultipliers;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Implementations.PlayerImplementation
{
    public class Player : Creature
    {
        [Inject]
        private void Construct(PlayerStatsMultiplier statsMultiplier)
        {
            stats.SetStatsMultiplier(statsMultiplier);
        }
    }
}