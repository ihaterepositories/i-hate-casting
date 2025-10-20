using Models.Items.Weapons.Base.Aiming.Base;
using Models.Items.Weapons.Base.StatsHandling;
using Models.WorldObjects.Creatures.PlayerImpl.DataContainers;
using UnityEngine;

namespace Models.Items.Weapons.Base.Aiming
{
    public class PlayerFollowingAimer : Aimer
    {
        private readonly PlayerPositionTracker _playerPositionTracker;
        
        public PlayerFollowingAimer(
            WeaponStatsCalculator weaponStatsCalculator, 
            Transform weaponTransform,
            PlayerPositionTracker playerPositionTracker) : base(weaponStatsCalculator, weaponTransform)
        {
            _playerPositionTracker = playerPositionTracker;
        }

        protected override Vector3 GetTargetPosition()
        {
            return _playerPositionTracker.Position;
        }
    }
}