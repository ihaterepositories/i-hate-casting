using Models.UI.StatusTexts.Services.Visualizing.Interfaces;
using TMPro;

namespace Models.UI.StatusTexts.Services.Visualizing
{
    public class SimpleStatusTextVisualizer : IStatusTextVisualizeService
    {
        private readonly TextMeshProUGUI _capacityText;

        public SimpleStatusTextVisualizer(TextMeshProUGUI capacityText)
        {
            _capacityText = capacityText;
        }

        public void UpdateText(string text)
        {
            _capacityText.text = text;
        }
    }
}