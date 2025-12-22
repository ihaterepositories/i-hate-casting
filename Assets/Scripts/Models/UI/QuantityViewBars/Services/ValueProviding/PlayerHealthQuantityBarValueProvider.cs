using System;
using Models.Creatures.Services.Living.Interfaces;
using Models.UI.QuantityViewBars.Services.ValueProviding.Interfaces;

namespace Models.UI.QuantityViewBars.Services.ValueProviding
{
    public class PlayerHealthQuantityBarValueProvider : IQuantityBarValueProvider
    {
        private readonly ICreatureHealth _playerHealthService;

        public PlayerHealthQuantityBarValueProvider(ICreatureHealth playerHealthService)
        {
            _playerHealthService = playerHealthService;

            _playerHealthService.OnHealthChanged += RaiseOnValueChanged;
        }

        public event Action OnValueChanged;

        public Tuple<float, float> GetValue()
        {
            return new Tuple<float, float>(_playerHealthService.CurrentValue, _playerHealthService.MaxValue);
        }

        public void CleanResources()
        {
            _playerHealthService.OnHealthChanged -= RaiseOnValueChanged;
        }
        
        private void RaiseOnValueChanged()
        {
            OnValueChanged?.Invoke();
        }
    }
}