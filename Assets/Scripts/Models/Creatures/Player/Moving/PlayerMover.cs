using Core.Input.Interfaces;
using Models.Creatures.Player.Data;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Player.Moving
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private readonly float _speed = PlayerStats.Speed;
        
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
            rb.velocity = new Vector2(_horizontalAxis, _verticalAxis).normalized * _speed;
        }

        private void FlipSpriteToMoveDirection()
        {
            if (_horizontalAxis != 0)
                spriteRenderer.flipX = _horizontalAxis < 0;
        }
    }
}