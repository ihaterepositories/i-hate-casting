using System;

namespace Models.Creatures.Services.MoveBoosting.Interfaces
{
    public interface ICreatureMoveBooster
    {
        public float BoostCooldownDuration { get; }
        public float BoostCooldownTimeElapsed { get; }

        public event Action OnBoostActivated;
        
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