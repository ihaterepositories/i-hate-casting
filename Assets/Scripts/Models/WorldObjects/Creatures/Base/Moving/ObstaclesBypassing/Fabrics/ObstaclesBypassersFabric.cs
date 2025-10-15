using Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Constants;
using Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Enums;
using Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Interfaces;
using Models.WorldObjects.Creatures.PlayerImpl;
using Models.WorldObjects.Creatures.PlayerImpl.DataContainers;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.Moving.ObstaclesBypassing.Fabrics
{
    public class ObstaclesBypassersFabric
    {
        private readonly PlayerPositionTracker _playerPositionTracker;
        
        public ObstaclesBypassersFabric(PlayerPositionTracker playerPositionTracker)
        {
            _playerPositionTracker = playerPositionTracker;
        }
        
        public IObstaclesBypassService Create(ObstaclesBypassType obstaclesBypassType, Transform transform)
        {
            switch (obstaclesBypassType)
            {
                default:
                case ObstaclesBypassType.None:
                    return new ObstaclesBypasser(transform, ObstaclesBypassConfig.GetFor(ObstaclesBypassType.None), _playerPositionTracker);
                case ObstaclesBypassType.Light:
                    return new ObstaclesBypasser(transform, ObstaclesBypassConfig.GetFor(ObstaclesBypassType.Light), _playerPositionTracker);
                case ObstaclesBypassType.Heavy:
                    return new ObstaclesBypasser(transform, ObstaclesBypassConfig.GetFor(ObstaclesBypassType.Heavy), _playerPositionTracker);
            }
        }
    }
}