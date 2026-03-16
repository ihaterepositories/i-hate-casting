using System.Threading.Tasks;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Core.RoundBootstrapControl.Interfaces;
using Core.SpawnersControl.Interfaces;
using UnityEngine;
using Utils.Timers.Interfaces;
using Zenject;

namespace Core.RoundBootstrapControl
{
    [DisallowMultipleComponent]
    public class GameBootstrapper : MonoBehaviour, IGameBootstrapper
    {
        [SerializeField] private float _roundTime;
        
        private IEventBusInvoker _eventBusInvoker;
        private ISpawnersCreator _spawnersCreator;
        private ITimer _roundTimer;
        
        [Inject]
        private void Construct(
            IEventBusInvoker eventBusInvoker,
            ISpawnersCreator spawnersCreator,
            ITimer timer)
        {
            _eventBusInvoker = eventBusInvoker;
            _spawnersCreator = spawnersCreator;
            _roundTimer = timer;
        }

        private async void Start()
        {
            
        }

        private async Task StartGame()
        {
            await _spawnersCreator.CreateAsync();
            
            // Hide loading screen
            
            // Spawn player
            
            // Weapon select
            
            _roundTimer.Start(_roundTime);
            _eventBusInvoker.Invoke(new RoundTimerStarted(_roundTimer));
            
            // Delay before enemies spawn start
            await Task.Delay(3000);
            
            // Start enemies autospawners
        }
    }
}