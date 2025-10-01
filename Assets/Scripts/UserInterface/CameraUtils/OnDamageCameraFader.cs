using System.Collections;
using Models.Creatures.PlayerImpl;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Zenject;

namespace UserInterface.CameraUtils
{
    public class OnDamageCameraFader : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private PostProcessVolume _postProcessVolume;
        
        [Header("Settings")]
        [SerializeField] private float _intensity;
        [SerializeField] private float _fadeInSpeed;
        [SerializeField] private float _fadeOutSpeed;
        
        private Vignette _vignette;
        private Player _player;
        
        private bool _fadingIn;
        private bool _isAnimating;
        
        [Inject]
        private void Construct(Player player)
        {
            _player = player;
            
            _player.OnDamaged += FadeInVignette;
        }

        private void Awake()
        {
            _postProcessVolume.profile.TryGetSettings(out _vignette);
        }

        private void OnDisable()
        {
            _player.OnDamaged -= FadeInVignette;
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