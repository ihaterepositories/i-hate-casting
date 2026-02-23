using System;
using Core.AssetsLoading.PrefabsProviders.Interfaces;
using Core.Dtos;
using Models.Creatures;
using Spawners.Factories;
using UnityEngine;
using Zenject;

namespace Core
{
    [DisallowMultipleComponent]
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private SpawnerCreationData[] _creatureSpawnersCreationData;
        
        private IAssetsLoader _assetsLoader;
        private SpawnersFactory _spawnersFactory;

        /// <summary>
        /// Invokes when player has been spawned and pass a player object.
        /// </summary>
        public static event Action<Creature> OnPlayerSpawned;
        
        /// <summary>
        /// Invokes when player has been spawned without passing a player object.
        /// </summary>
        public static event Action OnPlayerSpawnedNotify;

        [Inject]
        private void Construct(
            IAssetsLoader assetsLoader,
            SpawnersFactory spawnersFactory)
        {
            _assetsLoader = assetsLoader;
            _spawnersFactory = spawnersFactory;
        }

        private async void Start()
        {
            
        }

        // private async void StartGame()
        // {
        //     var creatureSpawners =
        //         _configurablesSpawnersFactory.CreateSpawners<Creature, CreatureConfig, CreatureConfigKey>(_creatureSpawnersCreationData);
        //     
        //     _playerSpawner = _configurablesSpawnersFactory.CreatePlayerSpawner();
        //     _playerSpawner.OnModelSpawned += HandlePlayerSpawned;
        //     
        //     yield return _playerSpawner.LaunchSpawning();
        //     
        //     // Delay before enemies spawn start
        //     yield return new WaitForSeconds(3f);
        //
        //     foreach (var creatureSpawner in creatureSpawners)
        //     {
        //         StartCoroutine(creatureSpawner.LaunchSpawning());
        //     }
        // }
        //
        // private void HandlePlayerSpawned(Creature creature)
        // {
        //     Debug.Log($"Player spawned");
        //     _playerSpawner.OnModelSpawned -= HandlePlayerSpawned;
        //     OnPlayerSpawned?.Invoke(creature);
        //     OnPlayerSpawnedNotify?.Invoke();
        // }
    }
}