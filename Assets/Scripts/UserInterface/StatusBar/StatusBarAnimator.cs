using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.StatusBar
{
    public class StatusBarAnimator : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        protected Image Bar => _bar;

        protected void UpdateBarAnimated(float currentValue, float maxValue)
        {
            _bar.DOFade(0.5f, 0.2f)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    _bar.DOFillAmount(currentValue / maxValue, 0.2f)
                        .SetUpdate(true)
                        .OnComplete(() =>
                        {
                            _bar.DOFade(1f, 0.2f)
                                .SetUpdate(true);
                        });
                });
        }
        
        protected void UpdateBar(float currentValue, float maxValue)
        {
            _bar.fillAmount = currentValue / maxValue;
        }
    }
}