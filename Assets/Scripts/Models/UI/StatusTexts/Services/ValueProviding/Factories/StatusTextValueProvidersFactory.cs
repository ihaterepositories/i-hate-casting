using System;
using Core;
using Models.Creatures;
using Models.UI.StatusTexts.Services.ValueProviding.Enums;
using Models.UI.StatusTexts.Services.ValueProviding.Interfaces;

namespace Models.UI.StatusTexts.Services.ValueProviding.Factories
{
    public class StatusTextValueProvidersFactory
    {
        private Creature _player;
        private bool _isInitialized;

        public StatusTextValueProvidersFactory()
        {
            GameBootstrapper.OnPlayerSpawned += Initialize;
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
        
        private void Initialize(Creature player)
        {
            GameBootstrapper.OnPlayerSpawned -= Initialize;
            _player = player;
            _isInitialized = true;
        }
    }
}