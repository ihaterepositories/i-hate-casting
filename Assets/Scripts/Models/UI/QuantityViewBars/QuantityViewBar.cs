using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using DG.Tweening;
using Models.UI.QuantityViewBars.Services.ValueProviding.Enums;
using Models.UI.QuantityViewBars.Services.ValueProviding.Factories;
using Models.UI.QuantityViewBars.Services.ValueProviding.Interfaces;
using Models.UI.QuantityViewBars.Services.Visualizing.Enums;
using Models.UI.QuantityViewBars.Services.Visualizing.Factories;
using Models.UI.QuantityViewBars.Services.Visualizing.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Models.UI.QuantityViewBars
{
    public class QuantityViewBar : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Image _barImage;
        
        [Header("Settings")]
        [SerializeField] private QuantityViewBarVisualizingType _visualizingType;
        [SerializeField] private QuantityViewBarValueResourceType _resourceType;

        private IEventBusSubscriber _eventBusSubscriber;
        
        // Factories
        private QuantityViewBarVisualizersFactory _quantityViewBarVisualizersFactory;
        private QuantityViewBarValueProvidersFactory _quantityViewBarValueProvidersFactory;
        
        // Services
        private IQuantityViewBarVisualizer _quantityViewBarVisualizer;
        private IQuantityBarValueProvider _quantityBarValueProvider;
        
        [Inject]
        private void Construct(
            IEventBusSubscriber eventBusSubscriber,
            QuantityViewBarValueProvidersFactory quantityViewBarValueProvidersFactory,
            QuantityViewBarVisualizersFactory quantityViewBarVisualizersFactory)
        {
            _eventBusSubscriber = eventBusSubscriber;
            _quantityViewBarValueProvidersFactory = quantityViewBarValueProvidersFactory;
            _quantityViewBarVisualizersFactory = quantityViewBarVisualizersFactory;
        }

        private void OnEnable()
        {
            _eventBusSubscriber.Subscribe<PlayerSpawnedSignal>(Initialize);
        }

        private void OnDisable()
        {
            _quantityBarValueProvider.OnValueChanged -= UpdateBar;
            _quantityBarValueProvider.CleanResources();
            _barImage.DOKill();
        }

        private void Initialize(PlayerSpawnedSignal playerSpawnedSignal)
        {
            _eventBusSubscriber.Unsubscribe<PlayerSpawnedSignal>(Initialize);
            _quantityBarValueProvider = _quantityViewBarValueProvidersFactory.Create(_resourceType);
            _quantityViewBarVisualizer = _quantityViewBarVisualizersFactory.Create(_visualizingType, _barImage, _quantityBarValueProvider);   
            _quantityBarValueProvider.OnValueChanged += UpdateBar;
        }
        
        private void UpdateBar()
        {
            _quantityViewBarVisualizer.UpdateBar();
        }
    }
}