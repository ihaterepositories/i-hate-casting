using Core;
using Core.Input.Interfaces;
using Models.Creatures;
using Models.Weapons.Services.Aiming.Enums;
using Models.Weapons.Services.Aiming.Interfaces;
using Models.Weapons.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Weapons.Services.Aiming.Factories
{
    public class WeaponAimersFactory
    {
        private readonly IInputHandler _inputHandler;
        private Transform _playerTransform;
        
        public WeaponAimersFactory(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            GameBootstrapper.OnPlayerSpawned += Initialize;
        }
        
        public IAimService Create(AimType aimType, IWeaponStatsCalculator weaponStatsCalculator, Transform weaponTransform)
        {
            return aimType switch
            {
                AimType.PointerFollowing => new PointerFollowingAimer(weaponStatsCalculator, weaponTransform, _inputHandler),
                AimType.PlayerFollowing => new PlayerFollowingAimer(weaponStatsCalculator, weaponTransform, _playerTransform),
                _ => null
            };
        }
        
        private void Initialize(Creature player)
        {
            _playerTransform = player.transform;
            GameBootstrapper.OnPlayerSpawned -= Initialize;
        }
    }
}