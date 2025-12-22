namespace Models.Weapons.Services.StatsCalculating.Interfaces
{
    public interface IWeaponStatsCalculator
    {
        public float CalculateRange();
        
        public int CalculateMagazineCapacity();

        public float CalculateReloadTime();
        
        /// <returns>Random value between -spread degree and +spread degree from weapon stats.</returns>
        public float CalculateSpreadDegree();
        
        public float CalculateDamageToDeal();

        public float CalculateSpeed();

        public float CalculateCooldownTime();
    }
}