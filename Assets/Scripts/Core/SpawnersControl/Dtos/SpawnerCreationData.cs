using System;
using Spawners.Services.Instantiaters.Enums;
using Spawners.Services.SpawnPositionCalculators.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.SpawnersControl.Dtos
{
    [Serializable]
    public class SpawnerCreationData
    {
        [SerializeField] private AssetReferenceGameObject _prefabToSpawn;
        [SerializeField] private InstantiatingType _instantiatingType;
        [SerializeField] private SpawnPositionType _spawnPositionType;
        
        public AssetReferenceGameObject PrefabToSpawn => _prefabToSpawn;
        public InstantiatingType InstantiatingType => _instantiatingType;
        public SpawnPositionType SpawnPositionType => _spawnPositionType;
    }
}