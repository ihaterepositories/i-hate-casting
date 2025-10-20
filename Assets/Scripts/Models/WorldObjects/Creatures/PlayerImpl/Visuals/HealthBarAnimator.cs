using DG.Tweening;
using Models.WorldObjects.Creatures.Base.Living.Interfaces;
using TMPro;
using UnityEngine;
using UserInterface.StatusBar;
using Zenject;

namespace Models.WorldObjects.Creatures.PlayerImpl.Visuals
{
    public class HealthBarAnimator : StatusBarAnimator
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private IHealthService _healthService;

        [Inject]
        private void Construct(Player player)
        {
            _healthService = player.Health;
            
            _healthService.OnHealthChanged += UpdateBarAnimated;
            _healthService.OnHealthChanged += UpdateText;
            
            UpdateText(_healthService.CurrentValue, _healthService.MaxValue);
        }
        
        private void OnDisable()
        {
            _healthService.OnHealthChanged -= UpdateBarAnimated;
            _healthService.OnHealthChanged -= UpdateText;

            Bar.DOKill();
        }
        
        private void UpdateText(float currentValue, float maxValue)
        {
            _text.text = $"{currentValue/maxValue}";
        }
    }
}