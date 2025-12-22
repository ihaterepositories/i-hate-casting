using Models.Creatures.Services.Living.Enums;
using Models.Creatures.Services.Living.Interfaces;
using Models.Creatures.Services.StatsCalculating.Interfaces;

namespace Models.Creatures.Services.Living.Factories
{
    public class CreatureHealthServicesFactory
    {
        public ICreatureHealth Create(CreatureHealthType creatureHealthType, ICreatureStatsCalculator statsCalculateService)
        {
            return creatureHealthType switch
            {
                CreatureHealthType.Default => new DefaultCreatureHealth(statsCalculateService),
                _ => (ICreatureHealth)null
            };
        }
    }
}