using DG.Tweening;
using Models.WorldObjects.Creatures.PlayerImpl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.GameScreenWidgets.HealthWidget
{
    public class HealthCapacityWidget : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Image _healthCapacityImage;
        [SerializeField] private TextMeshProUGUI _healthCapacityText;
        
        private Player _player;
        
        [Inject]
        private void Construct(Player player)
        {
            _player = player;
            
            _player.OnDamaged += UpdateHealthCapacityView;
        }

        private void Start()
        {
            _healthCapacityText.text = $"{_player.Health}/{_player.StatsCalculator.GetMaxHealth()}";
        }
        
        private void OnDisable()
        {
            _player.OnDamaged -= UpdateHealthCapacityView;
            
            _healthCapacityImage.DOKill();
        }

        private void UpdateHealthCapacityView()
        {
            _healthCapacityText.text = $"{_player.Health}/{_player.StatsCalculator.GetMaxHealth()}";
            var fillAmount = _player.Health / _player.StatsCalculator.GetMaxHealth();
            
            _healthCapacityImage.DOFade(0.5f, 0.2f)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    _healthCapacityImage.DOFillAmount(fillAmount, 0.2f)
                        .SetUpdate(true)
                        .OnComplete(() =>
                        {
                            _healthCapacityImage.DOFade(1f, 0.2f)
                                .SetUpdate(true);
                        });
                });
        }
    }
}