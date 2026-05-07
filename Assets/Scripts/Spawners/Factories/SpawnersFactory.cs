using System;
using Shared.Models.PoolableMonoBehaviours;
using Spawners.Interfaces;
using Spawners.Services.Instantiaters.Enums;
using Spawners.Services.Instantiaters.Factories;
using Spawners.Services.SpawnPositionCalculators.Enums;
using Spawners.Services.SpawnPositionCalculators.Factories;
using UnityEngine;

namespace Spawners.Factories
{
    public class SpawnersFactory
    {
        private readonly InstantiatersFactory _instantiatersFactory;
        private readonly SpawnPositionCalculatorsFactory _spawnPositionCalculatorsFactory;

        public SpawnersFactory(
            InstantiatersFactory instantiatersFactory,
            SpawnPositionCalculatorsFactory spawnPositionCalculatorsFactory)
        {
            _instantiatersFactory = instantiatersFactory;
            _spawnPositionCalculatorsFactory = spawnPositionCalculatorsFactory;
        }

        public ISpawner<T> CreateSpawner<T>(
            GameObject prefab,
            InstantiatingType instantiatingType,
            SpawnPositionType spawnPositionType
            ) where T : PoolableMonoBehaviour
        {
            return new Spawner<T>(
                _instantiatersFactory.Create<T>(instantiatingType, prefab),
                _spawnPositionCalculatorsFactory.Create(spawnPositionType));
        }
    }
}