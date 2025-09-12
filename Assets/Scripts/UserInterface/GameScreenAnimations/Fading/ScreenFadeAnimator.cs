using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.GameScreenAnimations.Fading
{
    public class ScreenFadeAnimator : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Image _screenFadePanel;
        
        [Header("Settings")]
        [SerializeField] private float _fadeInAlpha = 0.85f;
        [SerializeField] private float _fadeInDuration = 0.5f;
        [SerializeField] private float _fadeOutDuration = 0.25f;

        public void FadeIn()
        {
            _screenFadePanel.DOFade(_fadeInAlpha, _fadeInDuration).SetUpdate(true);
        }
        
        public void FadeOut()
        {
            _screenFadePanel.DOFade(0f, _fadeOutDuration).SetUpdate(true);
        }
    }
}