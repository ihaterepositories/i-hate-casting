using UnityEngine;

namespace UserInterface.CameraUtils
{
    public class CameraMoveZoneLimiter : MonoBehaviour
    {
        [Header("Left down edge:")]
        [SerializeField] private Vector2 _minBounds;
        [Header("Right up edge:")]
        [SerializeField] private Vector2 _maxBounds;

        private UnityEngine.Camera _camera;
        
        private float _camHalfHeight;
        private float _camHalfWidth;

        private void Start()
        {
            _camera = UnityEngine.Camera.main;

            _camHalfHeight = _camera.orthographicSize;
            _camHalfWidth = _camera.aspect * _camHalfHeight;
        }

        private void LateUpdate()
        {
            Vector3 pos = transform.position;
            
            pos.x = Mathf.Clamp(pos.x, _minBounds.x + _camHalfWidth, _maxBounds.x - _camHalfWidth);
            pos.y = Mathf.Clamp(pos.y, _minBounds.y + _camHalfHeight, _maxBounds.y - _camHalfHeight);

            transform.position = pos;
        }
    }
}