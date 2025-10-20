using Core.Input.Interfaces;
using Models.Items.Weapons.Base.Aiming.Base;
using Models.Items.Weapons.Base.Aiming.Enums;
using Models.Items.Weapons.Base.Aiming.Interfaces;
using Models.Items.Weapons.Base.StatsHandling;
using Models.WorldObjects.Creatures.PlayerImpl.DataContainers;
using UnityEngine;

namespace Models.Items.Weapons.Base.Aiming.Factories
{
    public class AimersFactory
    {
        private readonly IInputHandler _inputHandler;
        private readonly PlayerPositionTracker _playerPositionTracker;
        
        public AimersFactory(
            IInputHandler inputHandler,
            PlayerPositionTracker playerPositionTracker)
        {
            _inputHandler = inputHandler;
            _playerPositionTracker = playerPositionTracker;
        }
        
        public IAimService Create(AimType aimType, WeaponStatsCalculator weaponStatsCalculator, Transform weaponTransform)
        {
            return aimType switch
            {
                AimType.PointerFollowing => new PointerFollowingAimer(weaponStatsCalculator, weaponTransform, _inputHandler),
                AimType.PlayerFollowing => new PlayerFollowingAimer(weaponStatsCalculator, weaponTransform, _playerPositionTracker),
                _ => null
            };
        }
    }
}