using Core;
using Models.Creatures.Services.ObstaclesBypassing.Dtos;
using Models.Creatures.Services.ObstaclesBypassing.Enums;
using Models.Creatures.Services.ObstaclesBypassing.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.ObstaclesBypassing.Factories
{
    public class CreatureObstaclesBypassersFactory
    {
        private Transform _playerTransform;
        
        public CreatureObstaclesBypassersFactory()
        {
            GameBootstrapper.OnPlayerSpawned += Initialize;
        }
        
        public IObstaclesBypassForCreatureService Create(CreatureObstaclesBypassType creatureObstaclesBypassType, Transform transform)
        {
            switch (creatureObstaclesBypassType)
            {
                default:
                case CreatureObstaclesBypassType.None:
                    return new ForCreatureObstaclesBypasser(transform, _playerTransform, ObstaclesBypassConfig.GetFor(CreatureObstaclesBypassType.None));
                case CreatureObstaclesBypassType.Light:
                    return new ForCreatureObstaclesBypasser(transform, _playerTransform, ObstaclesBypassConfig.GetFor(CreatureObstaclesBypassType.Light));
                case CreatureObstaclesBypassType.Heavy:
                    return new ForCreatureObstaclesBypasser(transform, _playerTransform, ObstaclesBypassConfig.GetFor(CreatureObstaclesBypassType.Heavy));
            }
        }
        
        private void Initialize(Creature player)
        {
            _playerTransform = player.transform;
            GameBootstrapper.OnPlayerSpawned -= Initialize;
        }
    }
}