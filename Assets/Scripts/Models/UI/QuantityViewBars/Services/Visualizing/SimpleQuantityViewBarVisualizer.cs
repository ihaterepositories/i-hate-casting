using Models.UI.QuantityViewBars.Services.ValueProviding.Interfaces;
using Models.UI.QuantityViewBars.Services.Visualizing.Interfaces;
using UnityEngine.UI;

namespace Models.UI.QuantityViewBars.Services.Visualizing
{
    public class SimpleQuantityViewBarVisualizer : IQuantityViewBarVisualizer
    {
        private readonly Image _bar;
        private readonly IQuantityBarValueProvider _valueProvider;
        
        public SimpleQuantityViewBarVisualizer(
            Image statusBar,
            IQuantityBarValueProvider valueProvider)
        {
            _bar = statusBar;
            _valueProvider = valueProvider;
        }
        
        public void UpdateBar()
        {
            var value = _valueProvider.GetValue();
            _bar.fillAmount = value.Item1 / value.Item2;
        }
    }
}