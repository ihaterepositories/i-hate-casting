using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Interfaces
{
    public interface IObstaclesBypassService
    {
        public Vector2 CalculateBypassDirection();
    }
}