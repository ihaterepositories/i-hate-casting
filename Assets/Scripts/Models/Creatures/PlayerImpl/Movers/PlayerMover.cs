using System;
using System.Collections;
using Core.GameControl;
using Core.Input.Interfaces;
using Models.Creatures.Base.StatsHandling;
using UnityEngine;
using Zenject;

namespace Models.Creatures.PlayerImpl.Movers
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private CreatureStatsCalculator _playerStatsCalculator => _player.StatsCalculator;
        
        private float _burstCoefficient = 1f;
        private bool _isBurstCooldowning;
        
        private IInputHandler _inputHandler;
        private float _horizontalAxis;
        private float _verticalAxis;
        
        public bool IsMoving => _rb.velocity.sqrMagnitude > 0.01f;
        public float BurstCooldownTime => _playerStatsCalculator.GetBurstCooldownTime();
        
        public event Action OnBurstActivated; 
        
        [Inject]
        public void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        private void Update()
        {
            if (GamePauser.IsGamePaused) return;
            
            SetDirectionByInput();
            
            // Burst activation
            if (_inputHandler.IsBurstButtonPressed() && 
                !_isBurstCooldowning &&
                IsMoving)
            {
                IncreaseBurstCoefficientWithinBurstDuration();
            }
        }

        private void FixedUpdate()
        {
            if (GamePauser.IsGamePaused) return;
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
            OnBurstActivated?.Invoke();
            StartCoroutine(BurstCoefficientIncreaseWithinBurstDurationCoroutine());
            StartCoroutine(BurstCooldownCoroutine());
        }

        private IEnumerator BurstCoefficientIncreaseWithinBurstDurationCoroutine()
        {
            _burstCoefficient = _playerStatsCalculator.GetWhileBurstSpeedIncreaseCoefficient();
            yield return new WaitForSeconds(_playerStatsCalculator.GetBurstDuration());
            _burstCoefficient = 1f;
        }

        private IEnumerator BurstCooldownCoroutine()
        {
            _isBurstCooldowning = true;
            yield return new WaitForSeconds(_playerStatsCalculator.GetBurstCooldownTime());
            _isBurstCooldowning = false;
        }
    }
}