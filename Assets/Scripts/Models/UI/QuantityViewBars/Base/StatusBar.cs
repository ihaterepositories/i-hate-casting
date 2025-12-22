using Core;
using DG.Tweening;
using Models.UI.QuantityViewBars.Services.ValueProviding.Factories;
using Models.UI.QuantityViewBars.Services.ValueProviding.Interfaces;
using Models.UI.QuantityViewBars.Services.Visualizing.Factories;
using Models.UI.QuantityViewBars.Services.Visualizing.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Models.UI.QuantityViewBars.Base
{
    public abstract class StatusBar : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] protected Image _barImage;
        
        // Factories
        protected QuantityViewBarVisualizersFactory _quantityViewBarVisualizersFactory;
        protected QuantityVewBarValueProvidersFactory _quantityVewBarValueProvidersFactory;
        
        // Services
        protected IQuantityViewBarVisualizer _quantityViewBarVisualizer;
        protected IQuantityBarValueProvider _quantityBarValueProvider;
        
        protected void ConstructBase(
            QuantityVewBarValueProvidersFactory quantityVewBarValueProvidersFactory,
            QuantityViewBarVisualizersFactory quantityViewBarVisualizersFactory)
        {
            _quantityVewBarValueProvidersFactory = quantityVewBarValueProvidersFactory;
            _quantityViewBarVisualizersFactory = quantityViewBarVisualizersFactory;
        }

        protected void OnEnableBase()
        {
            GameBootstrapper.OnPlayerSpawnedNotify += Initialize;
        }

        protected void OnDisableBase()
        {
            _quantityBarValueProvider.OnValueChanged -= UpdateBar;
            _quantityBarValueProvider.CleanResources();
            _barImage.DOKill();
        }

        protected abstract void Initialize();

        protected void UpdateBar()
        {
            _quantityViewBarVisualizer.UpdateBar();
        }
    }
}