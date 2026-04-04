using Shared.Services.SpriteFlippers.Interfaces;
using UnityEngine;

namespace Shared.Services.SpriteFlippers
{
    public class RigidBodiedSpriteFlipper : ISpriteFlipper
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly SpriteRenderer _spriteRenderer;
        
        private bool _isFlipped;

        public RigidBodiedSpriteFlipper(
            Rigidbody2D rigidbody, 
            SpriteRenderer spriteRenderer)
        {
            _rigidbody = rigidbody;
            _spriteRenderer = spriteRenderer;
        }
        
        // The _isFlipped flag prevents unnecessary sprite flipping each frame
        // when the sprite is already facing the correct direction.
        public void Tick()
        {
            if (_rigidbody.linearVelocity.x < 0 && !_isFlipped)
            {
                _spriteRenderer.flipX = true;
                _isFlipped = true;
            }
            else if (_rigidbody.linearVelocity.x > 0 && _isFlipped)
            {
                _spriteRenderer.flipX = false;
                _isFlipped = false;
            }
        }
    }
}