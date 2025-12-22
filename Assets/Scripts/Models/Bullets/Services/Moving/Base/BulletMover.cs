using UnityEngine;

namespace Models.Bullets.Services.Moving.Base
{
    /// <summary>
    /// Base class for bullet movers.
    /// </summary>
    public class BulletMover
    {
        protected readonly Rigidbody2D _rigidbody2D;
        protected readonly Transform _transform;
        protected float _moveSpeed;

        protected BulletMover(Rigidbody2D rigidbody2D, Transform transform, float moveSpeed)
        {
            _rigidbody2D = rigidbody2D;
            _transform = transform;
            _moveSpeed = moveSpeed;
        }
    }
}