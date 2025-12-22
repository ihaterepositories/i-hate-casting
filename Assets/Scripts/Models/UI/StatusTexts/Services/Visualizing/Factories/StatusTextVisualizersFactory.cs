using System;
using Models.UI.StatusTexts.Services.Visualizing.Enums;
using Models.UI.StatusTexts.Services.Visualizing.Interfaces;
using TMPro;

namespace Models.UI.StatusTexts.Services.Visualizing.Factories
{
    public class StatusTextVisualizersFactory
    {
        public IStatusTextVisualizeService Create(StatusTextVisualizingType statusTextVisualizingType, TextMeshProUGUI capacityText)
        {
            return statusTextVisualizingType switch
            {
                StatusTextVisualizingType.Simple => new SimpleStatusTextVisualizer(capacityText),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}