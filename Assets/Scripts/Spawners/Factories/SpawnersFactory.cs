using System;
using Spawners.Interfaces;
using Spawners.Services.Instantiaters.Enums;
using Spawners.Services.Instantiaters.Factories;
using Spawners.Services.SpawnBehaviourProviders.Enums;
using Spawners.Services.SpawnBehaviourProviders.Factories;
using Spawners.Services.SpawnPositionCalculators.Enums;
using Spawners.Services.SpawnPositionCalculators.Factories;
using Systems.Pooling.Models;
using UnityEngine;

namespace Spawners.Factories
{
    public class SpawnersFactory
    {
        private readonly InstantiatersFactory _instantiatersFactory;
        private readonly SpawnPositionCalculatorsFactory _spawnPositionCalculatorsFactory;
        private readonly SpawnBehaviourProvidersFactory _spawnBehaviourProvidersFactory;

        public SpawnersFactory(
            InstantiatersFactory instantiatersFactory,
            SpawnPositionCalculatorsFactory spawnPositionCalculatorsFactory,
            SpawnBehaviourProvidersFactory spawnBehaviourProvidersFactory)
        {
            _instantiatersFactory = instantiatersFactory;
            _spawnPositionCalculatorsFactory = spawnPositionCalculatorsFactory;
            _spawnBehaviourProvidersFactory = spawnBehaviourProvidersFactory;
        }

        public ISpawner<T> Create<T>(
            GameObject prefab,
            InstantiatingType instantiatingType,
            SpawnPositionType spawnPositionType, 
            SpawnBehaviourType spawnBehaviourType, 
            float delayBeforeNextSpawn
            ) where T : PoolableMonoBehaviour
        {
            if (delayBeforeNextSpawn < 0) throw new Exception("Delay before next spawn can't be less than 0!");
            
            return new Spawner<T>(
                _instantiatersFactory.Create<T>(instantiatingType, prefab),
                _spawnPositionCalculatorsFactory.Create(spawnPositionType),
                _spawnBehaviourProvidersFactory.Create(spawnBehaviourType),
                delayBeforeNextSpawn);
        }
    }
}