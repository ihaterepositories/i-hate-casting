using System;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UIServices.CountdownVisualizers.Base;
using UIServices.CountdownVisualizers.Interfaces;

namespace UIServices.CountdownVisualizers
{
    public class ThreeStepsCountdownVisualizer : CountdownVisualizer, ICountdownVisualizer
    {
        private CancellationTokenSource _cancellationTokenSource;

        public ThreeStepsCountdownVisualizer(TextMeshProUGUI text) : base(text)
        {
        }

        public event Action OnComplete;

        public void Visualize(float time)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            
            _ = VisualizeAsync(time, _cancellationTokenSource.Token);
        }
        
        private async Task VisualizeAsync(float time, CancellationToken cancellationToken)
        {
            try
            {
                FadeInText();
                
                SetText("3");
                await Task.Delay(TimeSpan.FromSeconds(time / 3), cancellationToken);
                SetText("2");
                await Task.Delay(TimeSpan.FromSeconds(time / 3), cancellationToken);
                SetText("1");
                await Task.Delay(TimeSpan.FromSeconds(time / 3), cancellationToken);
            }
            finally
            {
                FadeOutText();
                OnComplete?.Invoke();
            }
        }

        public void ForceCleanUp()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            
            ForceCleanUpText();
        }
    }
}