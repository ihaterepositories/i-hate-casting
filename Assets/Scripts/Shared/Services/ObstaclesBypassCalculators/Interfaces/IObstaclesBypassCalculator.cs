using UnityEngine;

namespace Shared.Services.ObstaclesBypassCalculators.Interfaces
{
    /// <summary>
    /// Calculates an additional direction vector used to bypass obstacles.
    /// The returned vector is intended to be combined with the object's
    /// primary movement direction to adjust its path and avoid collisions.
    /// </summary>
    public interface IObstaclesBypassCalculator
    {
        /// <summary>
        /// Returns a direction vector that helps to steer the object around obstacles.
        /// This vector should be added to the base movement direction before normalization.
        /// </summary>
        public Vector2 CalculateBypassDirection();
    }
}