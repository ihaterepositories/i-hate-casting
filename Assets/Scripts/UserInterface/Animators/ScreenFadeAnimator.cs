using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Animators
{
    public class ScreenFadeAnimator : MonoBehaviour
    {
        [SerializeField] private Image _screenFadePanel;

        public void FadeIn()
        {
            _screenFadePanel.DOFade(0.7f, 0.5f).SetUpdate(true);
        }
        
        public void FadeOut()
        {
            _screenFadePanel.DOFade(0f, 0.25f).SetUpdate(true);
        }
    }
}