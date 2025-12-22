using System;
using Systems.ResourcesCleaning.Interfaces;

namespace Models.UI.QuantityViewBars.Services.ValueProviding.Interfaces
{
    public interface IQuantityBarValueProvider : IResourceCleanable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Tuple object, where Item1: currentValue and Item2: maxValue.</returns>
        public Tuple<float, float> GetValue();
        
        public event Action OnValueChanged;
    }
}