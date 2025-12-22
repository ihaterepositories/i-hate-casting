using System;
using Models.Bullets.Services.Moving.Enums;
using Models.Bullets.Services.Moving.Interfaces;
using UnityEngine;

namespace Models.Bullets.Services.Moving.Factories
{
    public class BulletMoversFactory
    {
        public IBulletMoveService Create(
            BulletMoveType moveType, 
            Rigidbody2D rigidbody,
            Transform transform,
            float moveSpeed)
        {
            return moveType switch
            {
                BulletMoveType.Forward => new ForwardBulletMover(rigidbody, transform, moveSpeed),
                _ => throw new ArgumentOutOfRangeException(nameof(moveType), moveType, null)
            };
        }
    }
}