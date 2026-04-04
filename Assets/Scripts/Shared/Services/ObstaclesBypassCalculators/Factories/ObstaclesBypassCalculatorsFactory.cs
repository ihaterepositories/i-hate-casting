using System;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Shared.Services.ObstaclesBypassCalculators.Dtos;
using Shared.Services.ObstaclesBypassCalculators.Enums;
using Shared.Services.ObstaclesBypassCalculators.Interfaces;
using UnityEngine;

namespace Shared.Services.ObstaclesBypassCalculators.Factories
{
    public class ObstaclesBypassCalculatorsFactory
    {
        private readonly IEventBusSubscriber _eventBusSubscriber;
        
        private Transform _playerTransform;
        private bool _isInitialized;
        
        public ObstaclesBypassCalculatorsFactory(IEventBusSubscriber eventBusSubscriber)
        {
            _eventBusSubscriber = eventBusSubscriber;
            _eventBusSubscriber.Subscribe<PlayerSpawnedSignal>(Initialize);
        }
        
        public IObstaclesBypassCalculator Create(ObstaclesBypassStrength obstaclesBypassStrength, Transform transform)
        {
            if (!_isInitialized) 
                throw new Exception("ObstaclesBypassCalculatorsFactory is not initialized yet. Cannot create an object.");
            
            switch (obstaclesBypassStrength)
            {
                default:
                case ObstaclesBypassStrength.None:
                    return new ObstaclesBypassCalculator(transform, _playerTransform, ObstaclesBypassConfig.GetFor(ObstaclesBypassStrength.None));
                case ObstaclesBypassStrength.Light:
                    return new ObstaclesBypassCalculator(transform, _playerTransform, ObstaclesBypassConfig.GetFor(ObstaclesBypassStrength.Light));
                case ObstaclesBypassStrength.Heavy:
                    return new ObstaclesBypassCalculator(transform, _playerTransform, ObstaclesBypassConfig.GetFor(ObstaclesBypassStrength.Heavy));
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