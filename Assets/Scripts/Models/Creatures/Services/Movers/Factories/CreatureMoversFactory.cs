using System;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Core.Input.Interfaces;
using Models.Creatures.Dtos;
using Models.Creatures.Services.Movers.Enums;
using Models.Creatures.Services.Movers.Interfaces;
using Models.Creatures.Services.StatsScalers.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.Movers.Factories
{
    public class CreatureMoversFactory
    {
        private readonly IInputHandler _inputHandler;
        private readonly IEventBusSubscriber _eventBusSubscriber;
        
        private Transform _playerTransform;
        private bool _isInitialized;
        
        public CreatureMoversFactory(
            IInputHandler inputHandler,
            IEventBusSubscriber eventBusSubscriber)
        {
            _inputHandler = inputHandler;
            _eventBusSubscriber = eventBusSubscriber;
            
            _eventBusSubscriber.Subscribe<PlayerSpawnedSignal>(Initialize);
        }

        public ICreatureMover Create(
            CreatureMoveType creatureMoveType,
            Rigidbody2D rigidbody2D, 
            CreatureStats stats,
            ICreatureStatsScaler statsScaler,
            Transform transform)
        {
            if (!_isInitialized) 
                throw new Exception("CreatureMoversFactory is not initialized yet. Cannot create an object.");
            
            return creatureMoveType switch
            {
                CreatureMoveType.ByInput => new ByInputCreatureMover(rigidbody2D, stats, statsScaler, _inputHandler),
                CreatureMoveType.PlayerFollowing => new FollowingPlayerCreatureMover(rigidbody2D, stats, statsScaler, transform, _playerTransform),
                _ => throw new ArgumentOutOfRangeException(nameof(creatureMoveType), creatureMoveType, null)
            };
        }

        private void Initialize(PlayerSpawnedSignal playerSpawnedSignal)
        {
            _playerTransform = playerSpawnedSignal.Player.transform;
            _eventBusSubscriber.Unsubscribe<PlayerSpawnedSignal>(Initialize);
            _isInitialized = true;
        }
    }
}