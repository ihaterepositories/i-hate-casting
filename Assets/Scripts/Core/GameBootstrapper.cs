using System;
using System.Threading.Tasks;
using Core.AssetsLoaders.Interfaces;
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
        [SerializeField] private SpawnersInitializer _spawnersInitializer;
        
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
            SpawnersInitializer spawnersInitializer)
        {
            _spawnersInitializer = spawnersInitializer;
        }

        private async void Start()
        {
            
        }

        private async Task StartGame()
        {
            await _spawnersInitializer.Initialize();
            
            // Hide loading screen
            
            // Delay before enemies spawn start
            await Task.Delay(3000);
        
            
        }
        
        // private void HandlePlayerSpawned(Creature creature)
        // {
        //     Debug.Log($"Player spawned");
        //     _playerSpawner.OnModelSpawned -= HandlePlayerSpawned;
        //     OnPlayerSpawned?.Invoke(creature);
        //     OnPlayerSpawnedNotify?.Invoke();
        // }
    }
}