using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using UnityEngine;
using Zenject;

namespace Shared.Utils.Camera
{
    public class CameraMover : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _smoothTime = 0.3f; 
        [SerializeField] private Vector3 _offset;

        private IEventBusSubscriber _eventBusSubscriber;
        
        private Transform _target;
        private Vector3 _velocity = Vector3.zero;
        private float _startZ;

        [Inject]
        private void Construct(IEventBusSubscriber eventBusSubscriber)
        {
            _eventBusSubscriber = eventBusSubscriber;
            _eventBusSubscriber.Subscribe<PlayerSpawnedSignal>(Initialize);
        }
        
        private void Start()
        {
            _startZ = transform.position.z;
        }

        private void Update()
        {
            if (_target == null) return;
            
            Vector3 targetPosition = new Vector3(
                _target.position.x + _offset.x,
                _target.position.y + _offset.y,
                _startZ
            );
            
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        }

        private void OnDisable()
        {
            _eventBusSubscriber.Unsubscribe<PlayerSpawnedSignal>(Initialize);
        }

        private void Initialize(PlayerSpawnedSignal playerSpawnedSignal)
        {
            _target = playerSpawnedSignal.Player.transform;
        }
    }
}