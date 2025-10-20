using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.ObstaclesBypassing.Interfaces
{
    public interface IObstaclesBypassService
    {
        public Vector2 CalculateBypassDirection();
    }
}