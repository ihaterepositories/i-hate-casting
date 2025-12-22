using Models.Creatures.Dtos;
using Models.Creatures.Enums;
using Models.Creatures.Services.StatsCalculating.Interfaces;
using Models.Creatures.Services.StatsCalculating.StatsModifying.Providers;

namespace Models.Creatures.Services.StatsCalculating.Factories
{
    public class CreatureStatsCalculatorsFactory
    {
       private readonly CreatureStatsModifiersProvider _statsModifiersProvider;

       public CreatureStatsCalculatorsFactory(CreatureStatsModifiersProvider statsModifiersProvider)
       {
           _statsModifiersProvider = statsModifiersProvider;
       }

       public ICreatureStatsCalculator Create(CreatureType creatureType, CreatureStats creatureStats)
       {
           return new CreatureStatsCalculateService(creatureStats, _statsModifiersProvider.GetFor(creatureType));
       }
    }
}