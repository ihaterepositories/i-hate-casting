using Core;
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
        
        // Factories
        private CooldownViewBarValueProvidersFactory _valueProvidersFactory;
        
        // Services
        private ICooldownBarValueProvider _valueProvider;

        [Inject]
        private void Construct(CooldownViewBarValueProvidersFactory valueProvidersFactory)
        {
            _valueProvidersFactory = valueProvidersFactory;
        }

        private void Update()
        {
            if (_valueProvider == null) return;
            
            _barImage.fillAmount = _valueProvider.GetValue().Item1 / _valueProvider.GetValue().Item2;
        }

        private void OnEnable()
        {
            GameBootstrapper.OnPlayerSpawnedNotify += Initialize;
        }

        private void OnDisable()
        {
            _barImage.DOKill();
        }

        private void Initialize()
        {
            GameBootstrapper.OnPlayerSpawnedNotify -= Initialize;
            _valueProvider = _valueProvidersFactory.Create(_resourceType);
        }
    }
}