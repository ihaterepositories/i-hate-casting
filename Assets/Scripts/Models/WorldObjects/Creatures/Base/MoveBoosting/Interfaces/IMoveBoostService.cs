namespace Models.WorldObjects.Creatures.Base.MoveBoosting.Interfaces
{
    public interface IMoveBoostService
    {
        public float BoostCooldownDuration { get; }
        public float BoostCooldownTimeElapsed { get; }
        
        /// <summary>
        /// Invoke this method in the FixedUpdate
        /// to enable move boosting when the cooldown is over.
        /// </summary>
        public void EnableBoost();
        
        /// <summary>
        /// Invoke this method in the Update
        /// to handle boost timings.
        /// </summary>
        public void HandleTimings();
    }
}