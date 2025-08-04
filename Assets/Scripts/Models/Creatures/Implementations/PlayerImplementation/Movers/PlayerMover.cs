using Core.Input.Interfaces;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Implementations.PlayerImplementation.Movers
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private float Speed => player.stats.GetSpeed();
        
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
            rb.velocity = new Vector2(_horizontalAxis, _verticalAxis).normalized * Speed;
        }

        private void FlipSpriteToMoveDirection()
        {
            if (_horizontalAxis != 0)
                spriteRenderer.flipX = _horizontalAxis < 0;
        }
    }
}