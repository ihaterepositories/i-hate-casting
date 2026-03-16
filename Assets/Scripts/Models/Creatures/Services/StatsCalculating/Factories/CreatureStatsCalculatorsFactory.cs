using Models.Creatures.Dtos;
using Models.Creatures.Enums;
using Models.Creatures.Services.StatsCalculating.Interfaces;
using Models.Creatures.Services.StatsMultiplying.Providers;

namespace Models.Creatures.Services.StatsCalculating.Factories
{
    public class CreatureStatsCalculatorsFactory
    {
       private readonly CreatureStatsMultipliersProvider _statsMultipliersProvider;

       public CreatureStatsCalculatorsFactory(CreatureStatsMultipliersProvider statsMultipliersProvider)
       {
           _statsMultipliersProvider = statsMultipliersProvider;
       }

       public ICreatureStatsCalculator Create(CreatureType creatureType, CreatureStats creatureStats)
       {
           return new CreatureStatsCalculateService(creatureStats, _statsMultipliersProvider.GetFor(creatureType));
       }
    }
}