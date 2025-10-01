using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UserInterface.GameScreenAnimations.Fading;

namespace Core.GameControl
{
    public class GamePauser : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private ScreenFadeAnimator _screenFadeAnimator;
        [SerializeField] private TextMeshProUGUI _unpauseCountdownText;
        
        [Header("Settings")]
        [Header("Unpause duration builds from 3 steps, each step duration:")]
        [SerializeField] private float _unpauseStepDuration = 0.5f;

        private int _unpauseIndexer;

        public static bool IsGamePaused { get; private set; }

        public void PauseGame()
        {
            _screenFadeAnimator.FadeIn();
            Time.timeScale = 0;
            IsGamePaused = true;
        }
        
        public void UnpauseGame()
        {
            _unpauseIndexer = 0;
            _unpauseCountdownText.text = "3";
            _unpauseCountdownText.DOFade(1f, 0.5f).SetUpdate(true);
            StartCoroutine(UnpauseCoroutine());
        }

        private IEnumerator UnpauseCoroutine()
        {
            yield return new WaitForSecondsRealtime(_unpauseStepDuration);
            
            if (_unpauseIndexer < 3)
            {
                _unpauseIndexer++;
                _unpauseCountdownText.text = (3 - _unpauseIndexer).ToString();
                StartCoroutine(UnpauseCoroutine());
            }
            else
            {
                _unpauseCountdownText.DOFade(0f, 0.25f).SetUpdate(true);
                _screenFadeAnimator.FadeOut();
                Time.timeScale = 1;
                IsGamePaused = false;
            }
        }
    }
}