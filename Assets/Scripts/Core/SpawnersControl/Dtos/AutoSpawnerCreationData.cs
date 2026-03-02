using System;
using Spawners.Services.Instantiaters.Enums;
using Spawners.Services.SpawnBehaviourProviders.Enums;
using Spawners.Services.SpawnPositionCalculators.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Core.Dtos
{
    [Serializable]
    public class AutoSpawnerCreationData
    {
        [SerializeField] private AssetReferenceGameObject _prefabToSpawn;
        [SerializeField] private InstantiatingType _instantiatingType;
        [SerializeField] private SpawnPositionType _spawnPositionType;
        [SerializeField] private SpawnBehaviourType _spawnBehaviourType;
        [SerializeField] private float _spawnInterval;

        public AutoSpawnerCreationData(
            AssetReferenceGameObject prefabToSpawn,
            InstantiatingType instantiatingType,
            SpawnPositionType spawnPositionType, 
            SpawnBehaviourType spawnBehaviourType, 
            float spawnInterval)
        {
            _prefabToSpawn = prefabToSpawn;
            _instantiatingType = instantiatingType;
            _spawnPositionType = spawnPositionType;
            _spawnBehaviourType = spawnBehaviourType;
            _spawnInterval = spawnInterval;
        }
        
        public AssetReferenceGameObject PrefabToSpawn => _prefabToSpawn;
        public InstantiatingType InstantiatingType => _instantiatingType;
        public SpawnPositionType SpawnPositionType => _spawnPositionType;
        public SpawnBehaviourType SpawnBehaviourType => _spawnBehaviourType;
        public float SpawnInterval  => _spawnInterval;
    }
}