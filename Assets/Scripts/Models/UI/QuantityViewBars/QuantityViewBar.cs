using Core;
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
        [SerializeField] protected Image _barImage;
        
        [Header("Settings")]
        [SerializeField] private QuantityViewBarVisualizingType _visualizingType;
        [SerializeField] private QuantityViewBarValueResourceType _resourceType;

        // Factories
        private QuantityViewBarVisualizersFactory _quantityViewBarVisualizersFactory;
        private QuantityVewBarValueProvidersFactory _quantityVewBarValueProvidersFactory;
        
        // Services
        private IQuantityViewBarVisualizer _quantityViewBarVisualizer;
        private IQuantityBarValueProvider _quantityBarValueProvider;
        
        [Inject]
        private void Construct(
            QuantityVewBarValueProvidersFactory quantityVewBarValueProvidersFactory,
            QuantityViewBarVisualizersFactory quantityViewBarVisualizersFactory)
        {
            _quantityVewBarValueProvidersFactory = quantityVewBarValueProvidersFactory;
            _quantityViewBarVisualizersFactory = quantityViewBarVisualizersFactory;
        }

        private void OnEnable()
        {
            GameBootstrapper.OnPlayerSpawnedNotify += Initialize;
        }

        private void OnDisable()
        {
            _quantityBarValueProvider.OnValueChanged -= UpdateBar;
            _quantityBarValueProvider.CleanResources();
            _barImage.DOKill();
        }

        private void Initialize()
        {
            GameBootstrapper.OnPlayerSpawnedNotify -= Initialize;
            _quantityBarValueProvider = _quantityVewBarValueProvidersFactory.Create(_resourceType);
            _quantityViewBarVisualizer = _quantityViewBarVisualizersFactory.Create(_visualizingType, _barImage, _quantityBarValueProvider);   
            _quantityBarValueProvider.OnValueChanged += UpdateBar;
        }
        
        private void UpdateBar()
        {
            _quantityViewBarVisualizer.UpdateBar();
        }
    }
}