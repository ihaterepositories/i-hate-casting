using System;
using Core;
using Models.Creatures;
using Models.UI.QuantityViewBars.Services.ValueProviding.Enums;
using Models.UI.QuantityViewBars.Services.ValueProviding.Interfaces;

namespace Models.UI.QuantityViewBars.Services.ValueProviding.Factories
{
    public class QuantityVewBarValueProvidersFactory
    {
        private Creature _player;
        
        private bool _isInitialized;

        public QuantityVewBarValueProvidersFactory()
        {
            GameBootstrapper.OnPlayerSpawned += Initialize;
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

        private void Initialize(Creature player)
        {
            GameBootstrapper.OnPlayerSpawned -= Initialize;
            _player = player;
            _isInitialized = true;
        }
    }
}