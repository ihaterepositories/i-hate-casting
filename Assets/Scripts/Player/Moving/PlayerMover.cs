using Player.Moving.Interfaces;
using UnityEngine;
using Zenject;

namespace Player.Moving
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private IInputHandler _inputHandler;
        private float _horizontalAxis;
        private float _verticalAxis;
        
        [Inject]
        public void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        private void Update()
        {
            SetAxisValues();
            ChangeVelocityByInput();
            FlipSpriteToMoveDirection();
        }
        
        private void SetAxisValues()
        {
            _horizontalAxis = _inputHandler.GetHorizontalAxisValue();
            _verticalAxis = _inputHandler.GetVerticalAxisValue();
        }
        
        private void ChangeVelocityByInput()
        {
            Vector2 movementDirection = 
                Mathf.Abs(_horizontalAxis) > Mathf.Abs(_verticalAxis) ? 
                new Vector2(_horizontalAxis, 0) : 
                new Vector2(0, _verticalAxis);
            
            rb.velocity = movementDirection.normalized * speed;
        }
        
        private void FlipSpriteToMoveDirection()
        {
            if (Mathf.Abs(_horizontalAxis) > Mathf.Abs(_verticalAxis))
                spriteRenderer.flipX = _horizontalAxis < 0;
        }
    }
}