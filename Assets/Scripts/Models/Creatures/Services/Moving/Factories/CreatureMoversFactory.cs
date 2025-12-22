using System;
using Core;
using Core.Input.Interfaces;
using Models.Creatures.Services.Moving.Enums;
using Models.Creatures.Services.Moving.Interfaces;
using Models.Creatures.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.Moving.Factories
{
    public class CreatureMoversFactory
    {
        private readonly IInputHandler _inputHandler;
        private Transform _playerTransform;
        
        public CreatureMoversFactory(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            GameBootstrapper.OnPlayerSpawned += Initialize;
        }

        public ICreatureMover Create(
            CreatureMoveType creatureMoveType, 
            ICreatureStatsCalculator statsCalculateService,
            Rigidbody2D rigidbody2D, 
            Transform transform)
        {
            return creatureMoveType switch
            {
                CreatureMoveType.ByInput => new ByInputCreatureMover(statsCalculateService, rigidbody2D, _inputHandler),
                CreatureMoveType.PlayerFollowing => new FollowingPlayerCreatureMover(statsCalculateService, rigidbody2D, transform, _playerTransform),
                _ => throw new ArgumentOutOfRangeException(nameof(creatureMoveType), creatureMoveType, null)
            };
        }

        private void Initialize(Creature player)
        {
            _playerTransform = player.transform;
            GameBootstrapper.OnPlayerSpawned -= Initialize;
        }
    }
}