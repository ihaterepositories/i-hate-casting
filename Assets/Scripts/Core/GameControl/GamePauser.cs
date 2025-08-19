using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UserInterface.Animators;

namespace Core.GameControl
{
    public class GamePauser : MonoBehaviour
    {
        [SerializeField] private ScreenFadeAnimator _screenFadeAnimator;
        [SerializeField] private TextMeshProUGUI _unpauseCountdownText;

        private int _unpauseIndexer;
        
        public void PauseGame()
        {
            _screenFadeAnimator.FadeIn();
            Time.timeScale = 0;
            GameStateHolder.IsGamePaused = true;
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
            yield return new WaitForSecondsRealtime(1f);
            
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
                GameStateHolder.IsGamePaused = false;
            }
        }
    }
}