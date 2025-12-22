using DG.Tweening;
using Models.UI.QuantityViewBars.Services.ValueProviding.Interfaces;
using Models.UI.QuantityViewBars.Services.Visualizing.Interfaces;
using UnityEngine.UI;

namespace Models.UI.QuantityViewBars.Services.Visualizing
{
    public class SmoothlyQuantityViewBarVisualizer : IQuantityViewBarVisualizer
    {
        private readonly Image _bar;
        private readonly IQuantityBarValueProvider _valueProvider;
        
        public SmoothlyQuantityViewBarVisualizer(
            Image statusBar,
            IQuantityBarValueProvider valueProvider)
        {
            _bar = statusBar;
            _valueProvider = valueProvider;
        }
        
        public void UpdateBar()
        {
            var value = _valueProvider.GetValue();
            
            _bar.DOFade(0.5f, 0.2f)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    _bar.DOFillAmount(value.Item1 / value.Item2, 0.2f)
                        .SetUpdate(true)
                        .OnComplete(() =>
                        {
                            _bar.DOFade(1f, 0.2f)
                                .SetUpdate(true);
                        });
                });
        }
    }
}