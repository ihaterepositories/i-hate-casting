using Models.Creatures.Dtos;
using Models.Creatures.Services.Health.Enums;
using Models.Creatures.Services.Health.Interfaces;
using Models.Creatures.Services.StatsScalers.Interfaces;

namespace Models.Creatures.Services.Health.Factories
{
    public class CreatureHealthesFactory
    {
        public ICreatureHealth Create(
            CreatureHealthType creatureHealthType, 
            CreatureStats stats,
            ICreatureStatsScaler statsScaler)
        {
            return creatureHealthType switch
            {
                CreatureHealthType.Default => new DefaultCreatureHealth(stats, statsScaler),
                _ => (ICreatureHealth)null
            };
        }
    }
}