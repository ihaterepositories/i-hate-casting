using System;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Models.Creatures.Services.ObstaclesBypassing.Dtos;
using Models.Creatures.Services.ObstaclesBypassing.Enums;
using Models.Creatures.Services.ObstaclesBypassing.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.ObstaclesBypassing.Factories
{
    public class CreatureObstaclesBypassersFactory
    {
        private readonly IEventBusSubscriber _eventBusSubscriber;
        
        private Transform _playerTransform;
        private bool _isInitialized;
        
        public CreatureObstaclesBypassersFactory(IEventBusSubscriber eventBusSubscriber)
        {
            _eventBusSubscriber = eventBusSubscriber;
            _eventBusSubscriber.Subscribe<PlayerSpawnedSignal>(Initialize);
        }
        
        public IObstaclesBypassForCreatureService Create(CreatureObstaclesBypassType creatureObstaclesBypassType, Transform transform)
        {
            if (!_isInitialized) 
                throw new Exception("CreatureObstaclesBypassersFactory is not initialized yet. Cannot create an object.");
            
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
        
        private void Initialize(PlayerSpawnedSignal playerSpawnedSignal)
        {
            _playerTransform = playerSpawnedSignal.Player.transform;
            _eventBusSubscriber.Unsubscribe<PlayerSpawnedSignal>(Initialize);
            _isInitialized = true;
        }
    }
}