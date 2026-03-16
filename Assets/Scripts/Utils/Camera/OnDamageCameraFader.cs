using System.Collections;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Models.Creatures;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Zenject;

namespace Utils.Camera
{
    public class OnDamageCameraFader : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private PostProcessVolume _postProcessVolume;
        
        [Header("Settings")]
        [SerializeField] private float _intensity;
        [SerializeField] private float _fadeInSpeed;
        [SerializeField] private float _fadeOutSpeed;
        
        private IEventBusSubscriber _eventBusSubscriber;
        
        private Vignette _vignette;
        private Creature _player;
        
        private bool _fadingIn;
        private bool _isAnimating;
        
        [Inject]
        private void Construct(IEventBusSubscriber eventBusSubscriber)
        {
            _eventBusSubscriber = eventBusSubscriber;
            _eventBusSubscriber.Subscribe<PlayerSpawnedSignal>(Initialize);
        }

        private void Awake()
        {
            _postProcessVolume.profile.TryGetSettings(out _vignette);
        }

        private void OnDisable()
        {
            _eventBusSubscriber.Unsubscribe<PlayerSpawnedSignal>(Initialize);
            _player.Health.OnDamaged -= FadeInVignette;
        }

        private void Initialize(PlayerSpawnedSignal playerSpawnedSignal)
        {
            _player = playerSpawnedSignal.Player;
            _player.Health.OnDamaged += FadeInVignette;
        }

        private void FadeInVignette()
        {
            if (_isAnimating && _fadingIn)
            {
                return;
            }
            
            StopAllCoroutines();
            _isAnimating = true;
            _fadingIn = true;
            StartCoroutine(FadingCoroutine());
        }

        private IEnumerator FadingCoroutine()
        {
            if (_fadingIn)
            {
                _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, _intensity, Time.deltaTime * _fadeInSpeed);
                if (Mathf.Abs(_vignette.intensity.value - _intensity) < 0.01f)
                {
                    _fadingIn = false;
                    _vignette.intensity.value = _intensity;
                }
            }

            if (!_fadingIn)
            {
                _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, 0f, Time.deltaTime * _fadeOutSpeed);
                if (Mathf.Abs(_vignette.intensity.value) < 0.01f)
                {
                    _isAnimating = false;
                    _vignette.intensity.value = 0f;
                }
            }
            
            yield return null;
            
            if (_isAnimating)
                StartCoroutine(FadingCoroutine());
        }
    }
}