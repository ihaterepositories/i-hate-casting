using System;
using Models.Creatures.Services.Living.Interfaces;
using Models.UI.StatusTexts.Services.ValueProviding.Interfaces;

namespace Models.UI.StatusTexts.Services.ValueProviding
{
    public class StatusTextPlayerHealthValueProvider : IStatusTextValueProvideService
    {
        private readonly ICreatureHealth _playerHealth;

        public StatusTextPlayerHealthValueProvider(ICreatureHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _playerHealth.OnHealthChanged += RaiseValueChanged;
        }

        public event Action OnValueChanged;
        
        public void CleanResources()
        {
            _playerHealth.OnHealthChanged -= RaiseValueChanged;
        }

        public string GetValueForText()
        {
            return $"{_playerHealth.CurrentValue} / {_playerHealth.MaxValue}";
        }

        private void RaiseValueChanged()
        {
            OnValueChanged?.Invoke();
        }
    }
}