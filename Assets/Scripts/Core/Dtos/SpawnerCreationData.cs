using System;
using Spawners.Services.Instantiaters.Enums;
using Spawners.Services.SpawnBehaviourProviders.Enums;
using Spawners.Services.SpawnPositionCalculators.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Dtos
{
    [Serializable]
    public class SpawnerCreationData
    {
        [SerializeField] private AssetReferenceGameObject _gameObjectToSpawn;
        [SerializeField] private InstantiatingType _instantiatingType;
        [SerializeField] private SpawnPositionType _spawnPositionType;
        [SerializeField] private SpawnBehaviourType _spawnBehaviourType;
        [SerializeField] private float _spawnInterval;

        public SpawnerCreationData(
            AssetReferenceGameObject gameObjectToSpawn,
            InstantiatingType instantiatingType,
            SpawnPositionType spawnPositionType, 
            SpawnBehaviourType spawnBehaviourType, 
            float spawnInterval)
        {
            _gameObjectToSpawn = gameObjectToSpawn;
            _instantiatingType = instantiatingType;
            _spawnPositionType = spawnPositionType;
            _spawnBehaviourType = spawnBehaviourType;
            _spawnInterval = spawnInterval;
        }
        
        public AssetReferenceGameObject GameObjectToSpawn => _gameObjectToSpawn;
        public InstantiatingType InstantiatingType => _instantiatingType;
        public SpawnPositionType SpawnPositionType => _spawnPositionType;
        public SpawnBehaviourType SpawnBehaviourType => _spawnBehaviourType;
        public float SpawnInterval  => _spawnInterval;
    }
}