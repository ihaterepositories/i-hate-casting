using Models.Bullets.Services.Moving.Base;
using Models.Bullets.Services.Moving.Interfaces;
using UnityEngine;

namespace Models.Bullets.Services.Moving
{
    public class ForwardBulletMover : BulletMover, IBulletMoveService
    {
        public ForwardBulletMover(
            Rigidbody2D rigidbody2D, 
            Transform transform,
            float moveSpeed) : base(rigidbody2D, transform, moveSpeed)
        {
        }

        public void EnableMove()
        {
            _rigidbody2D.velocity = _transform.right * _moveSpeed;
        }

        public void UpdateSpeed(float newSpeed)
        {
            _moveSpeed = newSpeed;
        }
    }
}