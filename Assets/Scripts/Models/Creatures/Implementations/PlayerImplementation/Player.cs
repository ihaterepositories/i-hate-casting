using Models.Creatures.Base;
using Models.Creatures.Implementations.PlayerImplementation.StatsMultipliers;
using Zenject;

namespace Models.Creatures.Implementations.PlayerImplementation
{
    public class Player : Creature
    {
        [Inject]
        private void Construct(PlayerStatsMultiplier statsMultiplier)
        {
            _stats.SetStatsMultiplier(statsMultiplier);
        }
    }
}