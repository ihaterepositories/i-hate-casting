using System;

namespace Models.UI.CooldownViewBars.Services.ValueProviding.Interfaces
{
    public interface ICooldownBarValueProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Tuple object, where Item1: currentValue and Item2: maxValue.</returns>
        public Tuple<float, float> GetValue();
    }
}