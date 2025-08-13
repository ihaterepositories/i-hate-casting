using Core.Input.Interfaces;
using Core.RoundControl;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Models.Creatures.Implementations.PlayerImplementation.Movers
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class PlayerMover : MonoBehaviour
    {
        [FormerlySerializedAs("player")] [SerializeField] private Player _player;
        [FormerlySerializedAs("rb")] [SerializeField] private Rigidbody2D _rb;
        [FormerlySerializedAs("spriteRenderer")] [SerializeField] private SpriteRenderer _spriteRenderer;

        private float Speed => _player.Stats.GetSpeed();
        
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
            if (GameStateController.IsGamePaused()) return;
            
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
            _rb.velocity = new Vector2(_horizontalAxis, _verticalAxis).normalized * Speed;
        }

        private void FlipSpriteToMoveDirection()
        {
            if (_horizontalAxis != 0)
                _spriteRenderer.flipX = _horizontalAxis < 0;
        }
    }
}