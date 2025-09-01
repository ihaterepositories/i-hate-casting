using System.Collections;
using Core.GameControl;
using Core.Input.Interfaces;
using Models.Creatures.Base.StatsHandling;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Implementations.PlayerImplementation.Movers
{
    // TODO: Implement burst cooldown
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private CreatureStatsCalculator _playerStatsCalculator => _player.CreatureStatsCalculator;
        
        private float _speedIncreaseCoefficient = 1f;
        
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
            if (GameStateHolder.IsGamePaused) return;
            
            SetAxisValues();
            ChangeVelocityByInput();
            
            if (_inputHandler.IsBurstButtonPressed())
            {
                DoBurst();
            }
        }
        
        private void SetAxisValues()
        {
            _horizontalAxis = _inputHandler.GetHorizontalAxisValue();
            _verticalAxis = _inputHandler.GetVerticalAxisValue();
        }
        
        private void ChangeVelocityByInput()
        {
            _rb.velocity = new Vector2(_horizontalAxis, _verticalAxis).normalized * (_playerStatsCalculator.GetSpeed() * _speedIncreaseCoefficient);
        }
        
        private void DoBurst()
        {
            StartCoroutine(DoBurstCoroutine());
        }

        private IEnumerator DoBurstCoroutine()
        {
            _speedIncreaseCoefficient = _playerStatsCalculator.GetWhileBurstSpeedIncreaseCoefficient();
            yield return new WaitForSeconds(_playerStatsCalculator.GetBurstDuration());
            _speedIncreaseCoefficient = 1f;
        }
    }
}