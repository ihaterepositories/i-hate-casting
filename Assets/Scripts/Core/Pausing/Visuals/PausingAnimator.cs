using Core.Pausing.Constants;
using Core.Pausing.Interfaces;
using TMPro;
using UIServices.CountdownVisualizers.Enums;
using UIServices.CountdownVisualizers.Factories;
using UIServices.CountdownVisualizers.Interfaces;
using UIServices.ImageFadeAnimators.Enums;
using UIServices.ImageFadeAnimators.Factories;
using UIServices.ImageFadeAnimators.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Pausing.Visuals
{
    public class PausingAnimator : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Image _panelImage;
        [SerializeField] private TextMeshProUGUI _countdownText;
        
        [Header("Settings")]
        [SerializeField] private ImageFadingType _imageFadingType;
        [SerializeField] private CountdownType _resumeCountdownType;
        
        private IImageFadeAnimator _imageFadeAnimator;
        private ICountdownVisualizer _countdownVisualizer;
        
        private IPauser _pauser;

        [Inject]
        private void Construct(
            ImageFadeAnimatorsFactory imageFadeAnimatorsFactory,
            CountdownVisualizersFactory countdownVisualizersFactory,
            IPauser pauser)
        {
            _imageFadeAnimator = imageFadeAnimatorsFactory.Create(_imageFadingType, _panelImage);
            _countdownVisualizer = countdownVisualizersFactory.Create(_resumeCountdownType, _countdownText);
            _pauser = pauser;
        }
        
        private void OnEnable()
        {
            _pauser.OnPaused += HandlePauseVisuals;
            _pauser.OnResumeStarted += HandleResumeVisuals;
            _pauser.OnResumeCrashed += ForceCleanUpVisuals;
        }

        private void OnDisable()
        {
            _pauser.OnPaused -= HandlePauseVisuals;
            _pauser.OnResumeStarted -= HandleResumeVisuals;
            _pauser.OnResumeCrashed -= ForceCleanUpVisuals;
        }

        private void HandlePauseVisuals()
        {
            _imageFadeAnimator.FadeIn();
        }
        
        private void HandleResumeVisuals()
        {
            void OnCountdownComplete()
            {
                _imageFadeAnimator.FadeOut();
                _countdownVisualizer.OnComplete -= OnCountdownComplete;
            }

            _countdownVisualizer.OnComplete += OnCountdownComplete;
            _countdownVisualizer.Visualize(PausingConstants.RESUME_TIME);
        }
        
        private void ForceCleanUpVisuals()
        {
            _imageFadeAnimator.ForceCleanUp();
            _countdownVisualizer.ForceCleanUp();
        }
    }
}