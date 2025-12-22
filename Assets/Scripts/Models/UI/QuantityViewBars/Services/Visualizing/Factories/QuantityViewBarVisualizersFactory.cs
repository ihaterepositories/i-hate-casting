using System;
using Models.UI.QuantityViewBars.Services.ValueProviding.Interfaces;
using Models.UI.QuantityViewBars.Services.Visualizing.Enums;
using Models.UI.QuantityViewBars.Services.Visualizing.Interfaces;
using UnityEngine.UI;

namespace Models.UI.QuantityViewBars.Services.Visualizing.Factories
{
    public class QuantityViewBarVisualizersFactory
    {
        public IQuantityViewBarVisualizer Create(
            QuantityViewBarVisualizingType animatorType, 
            Image bar,
            IQuantityBarValueProvider valueProvider)
        {
            return animatorType switch
            {
                QuantityViewBarVisualizingType.Simple => new SimpleQuantityViewBarVisualizer(bar, valueProvider),
                QuantityViewBarVisualizingType.Smoothly => new SmoothlyQuantityViewBarVisualizer(bar, valueProvider),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}