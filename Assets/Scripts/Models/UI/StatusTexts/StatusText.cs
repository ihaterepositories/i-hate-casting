using Core;
using DG.Tweening;
using Models.UI.StatusTexts.Services.ValueProviding.Enums;
using Models.UI.StatusTexts.Services.ValueProviding.Factories;
using Models.UI.StatusTexts.Services.ValueProviding.Interfaces;
using Models.UI.StatusTexts.Services.Visualizing.Enums;
using Models.UI.StatusTexts.Services.Visualizing.Factories;
using Models.UI.StatusTexts.Services.Visualizing.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Models.UI.StatusTexts
{
    public class StatusText : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private TextMeshProUGUI _textMesh;
        
        [Header("Settings")]
        [SerializeField] private StatusTextValueResourceType _resourceType;
        [SerializeField] private StatusTextVisualizingType _visualizingType;
        
        // Factories
        private StatusTextValueProvidersFactory _valueProvidersFactory;
        private StatusTextVisualizersFactory _visualizersFactory;
        
        // Services
        private IStatusTextValueProvideService _statusTextValueProvider;
        private IStatusTextVisualizeService _statusTextVisualizer;

        [Inject]
        private void Construct(
            StatusTextValueProvidersFactory valueProvidersFactory,
            StatusTextVisualizersFactory visualizersFactory)
        {
            _valueProvidersFactory = valueProvidersFactory;
            _visualizersFactory = visualizersFactory;

            GameBootstrapper.OnPlayerSpawnedNotify += Initialize;
        }

        private void OnDisable()
        {
            _statusTextValueProvider.OnValueChanged -= UpdateText;
            _statusTextValueProvider.CleanResources();
            _textMesh.DOKill();
        }

        private void Initialize()
        {
            GameBootstrapper.OnPlayerSpawnedNotify -= Initialize;
            
            _statusTextValueProvider = _valueProvidersFactory.Create(_resourceType);
            _statusTextVisualizer = _visualizersFactory.Create(_visualizingType, _textMesh);
            
            _statusTextValueProvider.OnValueChanged += UpdateText;
            UpdateText();
        }

        private void UpdateText()
        {
            _statusTextVisualizer.UpdateText(_statusTextValueProvider.GetValueForText());
        }
    }
}