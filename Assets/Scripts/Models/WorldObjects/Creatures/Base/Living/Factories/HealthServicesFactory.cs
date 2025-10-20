using Models.WorldObjects.Creatures.Base.Living.Enums;
using Models.WorldObjects.Creatures.Base.Living.Interfaces;
using Models.WorldObjects.Creatures.Base.StatsHandling;

namespace Models.WorldObjects.Creatures.Base.Living.Factories
{
    public class HealthServicesFactory
    {
        public IHealthService Create(HealthType healthType, CreatureStatsCalculator statsCalculator)
        {
            return healthType switch
            {
                HealthType.Default => new DefaultHealth(statsCalculator),
                _ => (IHealthService)null
            };
        }
    }
}