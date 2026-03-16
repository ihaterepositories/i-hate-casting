using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using DG.Tweening;
using Models.UI.CooldownViewBars.Services.ValueProviding.Enums;
using Models.UI.CooldownViewBars.Services.ValueProviding.Factories;
using Models.UI.CooldownViewBars.Services.ValueProviding.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Models.UI.CooldownViewBars
{
    public class CooldownViewBar : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] protected Image _barImage;
        
        [Header("Settings")]
        [SerializeField] private CooldownViewBarValueResourceType _resourceType;

        private IEventBusSubscriber _eventBusSubscriber;
        
        // Factories
        private CooldownViewBarValueProvidersFactory _valueProvidersFactory;
        
        // Services
        private ICooldownBarValueProvider _valueProvider;

        [Inject]
        private void Construct(
            IEventBusSubscriber eventBusSubscriber,
            CooldownViewBarValueProvidersFactory valueProvidersFactory)
        {
            _eventBusSubscriber = eventBusSubscriber;
            _valueProvidersFactory = valueProvidersFactory;
            
            _eventBusSubscriber.Subscribe<PlayerSpawnedSignal>(Initialize);
        }

        private void Update()
        {
            if (_valueProvider == null) return;
            
            _barImage.fillAmount = _valueProvider.GetValue().Item1 / _valueProvider.GetValue().Item2;
        }

        private void OnDisable()
        {
            _barImage.DOKill();
        }

        private void Initialize(PlayerSpawnedSignal signal)
        {
            _eventBusSubscriber.Unsubscribe<PlayerSpawnedSignal>(Initialize);
            _valueProvider = _valueProvidersFactory.Create(_resourceType);
        }
    }
}