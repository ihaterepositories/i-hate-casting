using System;
using Spawners.Services.Instantiaters.Enums;
using UnityEngine.AddressableAssets;

namespace Core.SpawnersControl.Dtos
{
    [Serializable]
    public class SpawnerCreationData<T> where T : Enum
    {
        public readonly T prefabType;
        public readonly AssetReferenceGameObject PrefabToSpawn;
        public readonly InstantiatingType InstantiatingType;
    }
}