using Shared.Services.ObstaclesBypassCalculators.Interfaces;

namespace Models.Creatures.Services.Movers.Interfaces
{
    /// <summary>
    /// Handles movement for creature.
    /// </summary>
    public interface ICreatureMover
    {
        /// <summary>
        /// Shows if creature are moving now.
        /// </summary>
        public bool IsMoving { get; }
        
        /// <summary>
        /// Performs one fixed update step, monitoring move activation conditions
        /// and moves creature in specified way. 
        /// </summary>
        public void FixedTick();
        public void AssignObstaclesBypasser(IObstaclesBypassCalculator obstaclesBypassCalculator);
    }
}