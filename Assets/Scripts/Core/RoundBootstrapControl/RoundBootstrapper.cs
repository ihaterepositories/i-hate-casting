using System.Threading.Tasks;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Core.RoundBootstrapControl.Interfaces;
using Core.SpawnersControl.Interfaces;
using Core.TimerEngines.Interfaces;
using UnityEngine;
using Zenject;

namespace Core.RoundBootstrapControl
{
    [DisallowMultipleComponent]
    public class RoundBootstrapper : MonoBehaviour, IRoundBootstrapper
    {
        [SerializeField] private float _roundTime;
        
        private IEventBusInvoker _eventBusInvoker;
        private ISpawnersCreator _spawnersCreator;
        private ITimerEngine _timerEngine; 
        
        [Inject]
        private void Construct(
            IEventBusInvoker eventBusInvoker,
            ISpawnersCreator spawnersCreator,
            ITimerEngine timerEngine)
        {
            _eventBusInvoker = eventBusInvoker;
            _spawnersCreator = spawnersCreator;
            _timerEngine = timerEngine;
        }

        private async void Start()
        {
            
        }

        private async Task StartGame()
        {
            // show loading screen
            
            await _spawnersCreator.CreateAsync();
            
            // Spawn player
            
            // Hide loading screen
            
            // Weapon select
            
            // Round timer launch
            _timerEngine.StartTimer(
                _roundTime, 
                () => _eventBusInvoker.Invoke(new RoundTimeExpiredSignal()));
            
            // Delay before enemies spawn start
            await Task.Delay(3000);
            
            // Start enemies autospawners
        }
    }
}