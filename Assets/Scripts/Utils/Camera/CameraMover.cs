using Core;
using Models.Creatures;
using UnityEngine;

namespace Utils.Camera
{
    public class CameraMover : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _smoothTime = 0.3f; 
        [SerializeField] private Vector3 _offset;
        
        private Transform _target;

        private Vector3 _velocity = Vector3.zero;
        private float _startZ;

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

        private void OnEnable()
        {
            GameBootstrapper.OnPlayerSpawned += Initialize;
        }

        private void OnDisable()
        {
            GameBootstrapper.OnPlayerSpawned -= Initialize;
        }

        private void Initialize(Creature player)
        {
            _target = player.transform;
        }
    }
}