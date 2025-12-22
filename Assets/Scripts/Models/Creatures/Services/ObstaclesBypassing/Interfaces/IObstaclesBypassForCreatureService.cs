using UnityEngine;

namespace Models.Creatures.Services.ObstaclesBypassing.Interfaces
{
    public interface IObstaclesBypassForCreatureService
    {
        public Vector2 CalculateBypassDirection();
    }
}