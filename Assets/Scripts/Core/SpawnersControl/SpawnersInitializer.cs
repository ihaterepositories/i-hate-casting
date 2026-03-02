using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.AssetsLoaders.Interfaces;
using Core.Dtos;
using Models.Bullets;
using Models.Bullets.Enums;
using Spawners.Factories;
using Spawners.Interfaces;
using Systems.Pooling.Models;
using UnityEngine;
using Zenject;

namespace Core
{
    [DisallowMultipleComponent]
    public class SpawnersInitializer : MonoBehaviour
    {
        [Header("Spawners configuration")]
        [SerializeField] private List<SpawnerCreationData<BulletType>> _bulletSpawnersData;
        
        [Header("Autospawners configuration")]
        
        
        private IAssetsLoader _assetsLoader;
        private SpawnersFactory _spawnersFactory;
     
        // Spawners
        public Dictionary<BulletType, ISpawner<Bullet>> BulletSpawners {get; private set;}
        
        /// <summary>
        /// Invokes when spawners are ready to work.
        /// </summary>
        public event Action OnInitialized;

        [Inject]
        private void Construct(
            IAssetsLoader assetsLoader,
            SpawnersFactory spawnersFactory)
        {
            _assetsLoader = assetsLoader;
            _spawnersFactory = spawnersFactory;
        }

        public async Task Initialize()
        {
            foreach (var sd in _bulletSpawnersData)
            {
                await CreateSpawner<Bullet, BulletType>(
                    sd, spawner => BulletSpawners.Add(sd.prefabType, spawner));
            }
            
            OnInitialized?.Invoke();
        }

        private async Task CreateSpawner<T, TType>(
            SpawnerCreationData<TType> spawnerCreationData,
            Action<ISpawner<T>> onCreated)
        where T : PoolableMonoBehaviour
        where TType : Enum
        {
            var prefab = await _assetsLoader.LoadAssetAsync(spawnerCreationData.PrefabToSpawn, true);
            onCreated?.Invoke(_spawnersFactory.CreateSpawner<T>(prefab, spawnerCreationData.InstantiatingType));
        }
    }
}