using UnityEngine;

namespace Models.Creatures.Base.Visuals
{
    public class ToMoveDirectionSpriteFlipper : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rb;

        private void Update()
        {
            if (_rb != null)
            {
                if (_rb.velocity.x > 0.01f)
                    _spriteRenderer.flipX = false;
                else if (_rb.velocity.x < -0.01f)
                    _spriteRenderer.flipX = true;
            }
        }
    }
}