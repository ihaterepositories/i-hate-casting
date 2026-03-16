using System;
using Core.GameEventsControl.Interfaces;
using Core.GameEventsControl.Signals;
using Models.Creatures;
using Models.UI.StatusTexts.Services.ValueProviding.Enums;
using Models.UI.StatusTexts.Services.ValueProviding.Interfaces;

namespace Models.UI.StatusTexts.Services.ValueProviding.Factories
{
    public class StatusTextValueProvidersFactory
    {
        private IEventBusSubscriber _eventBusSubscriber;
        private Creature _player;
        
        private bool _isInitialized;

        public StatusTextValueProvidersFactory(IEventBusSubscriber eventBusSubscriber)
        {
            _eventBusSubscriber = eventBusSubscriber;
            _eventBusSubscriber.Subscribe<PlayerSpawnedSignal>(Initialize);
        }

        public IStatusTextValueProvideService Create(StatusTextValueResourceType resourceType)
        {
            if (!_isInitialized) throw new NullReferenceException("Factory is not initialized!");

            return resourceType switch
            {
                StatusTextValueResourceType.PlayerHealthValue => new StatusTextPlayerHealthValueProvider(_player.Health),
                // StatusTextValueResourceType.PlayerWeaponMagazineCapacityValue => expr,
                _ => throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null)
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