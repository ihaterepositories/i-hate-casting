using DG.Tweening;
using TMPro;

namespace UIServices.CountdownVisualizers.Base
{
    /// <summary>
    /// Base functionality for countdown visualizers.
    /// </summary>
    public class CountdownVisualizer
    {
        private readonly TextMeshProUGUI _text;
        private readonly float _fadeDuration = 0.2f;

        protected CountdownVisualizer(TextMeshProUGUI text)
        {
            _text = text;
        }
        
        protected void SetText(string number)
        {
            _text.text = number;
        }
        
        protected void FadeInText()
        {
            _text.DOFade(1f, _fadeDuration).SetUpdate(true);
        }
        
        protected void FadeOutText()
        {
            _text.DOFade(0f, _fadeDuration).SetUpdate(true);
        }

        protected void ForceCleanUpText()
        {
            _text.DOKill();
            _text.alpha = 0f;
        }
    }
}