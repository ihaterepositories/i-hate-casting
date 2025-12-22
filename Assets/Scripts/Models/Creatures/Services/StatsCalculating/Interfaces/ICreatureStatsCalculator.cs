namespace Models.Creatures.Services.StatsCalculating.Interfaces
{
    public interface ICreatureStatsCalculator
    {
        public float CalculateMaxHealth();
        public float CalculateSpeed();
        public float CalculateBoostDuration();
        public float CalculateBoostStrength();
        public float CalculateBoostCooldownTime();
    }
}