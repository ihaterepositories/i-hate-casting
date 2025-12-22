using System;
using Core;
using Models.Creatures;
using Models.UI.CooldownViewBars.Services.ValueProviding.Enums;
using Models.UI.CooldownViewBars.Services.ValueProviding.Interfaces;

namespace Models.UI.CooldownViewBars.Services.ValueProviding.Factories
{
    public class CooldownViewBarValueProvidersFactory
    {
        private Creature _player;
        
        private bool _isInitialized;

        public CooldownViewBarValueProvidersFactory()
        {
            GameBootstrapper.OnPlayerSpawned += Initialize;
        }
        
        public ICooldownBarValueProvider Create(CooldownViewBarValueResourceType resourceType)
        {
            if (!_isInitialized) throw new NullReferenceException("Factory is not initialized!");

            return resourceType switch
            {
                CooldownViewBarValueResourceType.PlayerBoostCooldown => new PlayerBoostCooldownBarValueProvider(_player.MoveBooster),
                // StatusBarCooldownValueResourceType.PlayerWeaponCooldown => expr,
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