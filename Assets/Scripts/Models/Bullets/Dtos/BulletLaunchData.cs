using UnityEngine;

namespace Models.Bullets.Dtos
{
    public class BulletLaunchData
    {
        public BulletLaunchData(
            float moveSpeed, 
            float moveRange, 
            float damageToDeal, 
            Vector2 launchPosition, 
            float rotationAngle, 
            float spreadAngle)
        {
            MoveSpeed = moveSpeed;
            MoveRange = moveRange;
            DamageToDeal = damageToDeal;
            LaunchPosition = launchPosition;
            RotationAngle = rotationAngle;
            SpreadAngle = spreadAngle;
        }
        
        public readonly float MoveSpeed;
        public readonly float MoveRange;
        public readonly float DamageToDeal;
        public readonly Vector2 LaunchPosition;
        public readonly float RotationAngle;
        public readonly float SpreadAngle;
    }
}