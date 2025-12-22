using TMPro;
using UIServices.CountdownVisualizers.Enums;
using UIServices.CountdownVisualizers.Interfaces;

namespace UIServices.CountdownVisualizers.Factories
{
    public class CountdownVisualizersFactory
    {
        public ICountdownVisualizer Create(CountdownType countdownType, TextMeshProUGUI countdownText)
        {
            return countdownType switch
            {
                CountdownType.ThreeSteps => new ThreeStepsCountdownVisualizer(countdownText),
                _ => null
            };
        }
    }
}