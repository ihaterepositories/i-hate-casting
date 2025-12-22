using System;

namespace UIServices.CountdownVisualizers.Interfaces
{
    public interface ICountdownVisualizer
    {
        public event Action OnComplete;
        
        public void Visualize(float time);
        
        /// <summary>
        /// Immediately clears visualizations.
        /// </summary>
        public void ForceCleanUp();
    }
}