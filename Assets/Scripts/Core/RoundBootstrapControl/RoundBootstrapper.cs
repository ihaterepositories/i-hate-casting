using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Core.RoundBootstrapControl.Interfaces;
using Core.SpawnersControl.Interfaces;
using Core.TimerEngines.Interfaces;
using Models.Creatures;
using Models.Creatures.Enums;
using Spawners.Interfaces;
using UnityEngine;
using Zenject;

namespace Core.RoundBootstrapControl
{
    [DisallowMultipleComponent]
    public class RoundBootstrapper : MonoBehaviour, IRoundBootstrapper, IInitializable
    {
        [SerializeField] private float _roundTime;
        
        private IEventBusInvoker _eventBusInvoker;
        private IEventBusSubscriber _eventBusSubscriber;
        private ISpawnersCreator _spawnersCreator;
        private ITimerEngine _timerEngine;

        private Dictionary<CreatureType, ISpawner<Creature>> _creaturesSpawners;
        
        [Inject]
        private void Construct(
            IEventBusInvoker eventBusInvoker,
            IEventBusSubscriber eventBusSubscriber,
            ISpawnersCreator spawnersCreator,
            ITimerEngine timerEngine)
        {
            _eventBusInvoker = eventBusInvoker;
            _eventBusSubscriber = eventBusSubscriber;
            _spawnersCreator = spawnersCreator;
            _timerEngine = timerEngine;
        }

        public void Initialize()
        {
            _eventBusSubscriber.Subscribe<CreatureSpawnersInitializedSignal>(SetCreatureSpawners);
            
            StartCoroutine(StartGameCoroutine());
        }

        private IEnumerator StartGameCoroutine()
        {
            // show loading screen
            
            yield return _spawnersCreator.CreateCoroutine();
            
            // Spawn player
            var player = _spawnersCreator.CreatureSpawners[CreatureType.Player].SpawnAndGet();
            _eventBusInvoker.Invoke(new PlayerSpawnedSignal(player));
            
            // Hide loading screen
            
            // Weapon select
            
            // Round timer launch
            // _timerEngine.StartTimer(
            //     _roundTime, 
            //     () => _eventBusInvoker.Invoke(new RoundTimeExpiredSignal()));
            
            // Delay before enemies spawn start
            yield return new WaitForSeconds(3f);

            // Start enemies autospawners
        }

        private void SetCreatureSpawners(CreatureSpawnersInitializedSignal signal)
        {
            _creaturesSpawners = signal.CreaturesSpawners;
            _eventBusSubscriber.Unsubscribe<CreatureSpawnersInitializedSignal>(SetCreatureSpawners);
            Debug.Log($"Set creature spawners");
        }
    }
}