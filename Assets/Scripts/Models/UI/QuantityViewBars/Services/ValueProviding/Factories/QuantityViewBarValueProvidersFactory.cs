using System;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Models.Creatures;
using Models.UI.QuantityViewBars.Services.ValueProviding.Enums;
using Models.UI.QuantityViewBars.Services.ValueProviding.Interfaces;

namespace Models.UI.QuantityViewBars.Services.ValueProviding.Factories
{
    public class QuantityViewBarValueProvidersFactory
    {
        private IEventBusSubscriber _eventBusSubscriber;
        private Creature _player;
        
        private bool _isInitialized;

        public QuantityViewBarValueProvidersFactory(IEventBusSubscriber eventBusSubscriber)
        {
            _eventBusSubscriber = eventBusSubscriber;
            _eventBusSubscriber.Subscribe<PlayerSpawnedSignal>(Initialize);
        }

        public IQuantityBarValueProvider Create(QuantityViewBarValueResourceType resourceType)
        {
            if (!_isInitialized) throw new NullReferenceException("Factory is not initialized!");

            return resourceType switch
            {
                QuantityViewBarValueResourceType.PlayerHealth => new PlayerHealthQuantityBarValueProvider(_player.Health),
                // StatusBarValueProvidingType.PlayerWeaponMagazineCapacity => expr,
                _ => throw new ArgumentOutOfRangeException(nameof(resourceType),
                    resourceType, null)
            };
        }

        private void Initialize(PlayerSpawnedSignal playerSpawnedSignal)
        {
            _eventBusSubscriber.Unsubscribe<PlayerSpawnedSignal>(Initialize);
            _player = playerSpawnedSignal.Player;
            _isInitialized = true;
        }
    }
}