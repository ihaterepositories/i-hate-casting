using System.Collections;
using Core.GameControl;
using Core.Input.Interfaces;
using Models.Creatures.Base.StatsHandling;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Implementations.PlayerImplementation.Movers
{
    // TODO: Implement burst cooldown
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private CreatureStatsCalculator _playerStatsCalculator => _player.StatsCalculator;
        
        private float _burstCoefficient = 1f;
        
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
            
            SetDirectionByInput();
            
            // Burst activation
            if (_inputHandler.IsBurstButtonPressed())
            {
                IncreaseBurstCoefficientWithinBurstDuration();
            }
        }

        private void FixedUpdate()
        {
            if (GameStateHolder.IsGamePaused) return;
            ChangeVelocityByInput();
        }

        private void SetDirectionByInput()
        {
            _horizontalAxis = _inputHandler.GetHorizontalAxisValue();
            _verticalAxis = _inputHandler.GetVerticalAxisValue();
        }
        
        private void ChangeVelocityByInput()
        {
            _rb.velocity = new Vector2(_horizontalAxis, _verticalAxis).normalized * (_playerStatsCalculator.GetSpeed() * _burstCoefficient);
        }
        
        private void IncreaseBurstCoefficientWithinBurstDuration()
        {
            StartCoroutine(BurstCoefficientIncreaseWithinBurstDurationCoroutine());
        }

        private IEnumerator BurstCoefficientIncreaseWithinBurstDurationCoroutine()
        {
            _burstCoefficient = _playerStatsCalculator.GetWhileBurstSpeedIncreaseCoefficient();
            yield return new WaitForSeconds(_playerStatsCalculator.GetBurstDuration());
            _burstCoefficient = 1f;
        }
    }
}