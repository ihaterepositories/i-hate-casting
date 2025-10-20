using System;
using Core.Input.Interfaces;
using Models.WorldObjects.Creatures.Base.MoveBoosting.Interfaces;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.MoveBoosting
{
    // TODO: refactor timers with asycnhronous tasks
    public class ByInputMoveBooster : IMoveBoostService
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly CreatureStatsCalculator _statsCalculator;
        private readonly IInputHandler _inputHandler;
        
        private float _boostCoefficient = 1f;
        private bool _isBoostCooldowning;
        
        private float _boostTimer;
        private float _boostCooldownTimer;
        
        private bool _isMoving => _rigidbody2D.velocity.sqrMagnitude > 0.01f;
        
        public event Action OnBurstActivated;
        
        public ByInputMoveBooster (
            Rigidbody2D rigidbody2D, 
            CreatureStatsCalculator statsCalculator,
            IInputHandler inputHandler)
        {
            _rigidbody2D = rigidbody2D;
            _statsCalculator = statsCalculator;
            _inputHandler = inputHandler;
        }

        public float BoostCooldownDuration => _statsCalculator.GetMoveBoostCooldownTime();
        public float BoostCooldownTimeElapsed => _statsCalculator.GetMoveBoostCooldownTime() - _boostCooldownTimer;

        public void EnableBoost()
        {
            if (_boostCoefficient <= 1f) return;
            _rigidbody2D.velocity *= _boostCoefficient;
        }
        
        public void HandleTimings()
        {
            // Burst activation
            if (_inputHandler.IsBurstButtonPressed() &&
                !_isBoostCooldowning &&
                _isMoving)
            {
                OnBurstActivated?.Invoke();
            
                _boostTimer = _statsCalculator.GetMoveBoostDuration();
                _boostCooldownTimer = _statsCalculator.GetMoveBoostCooldownTime();
            
                _boostCoefficient = _statsCalculator.GetMoveBoostCoefficient();
                _isBoostCooldowning = true;
            }

            // Handle burst timer
            if (_boostTimer > 0f)
            {
                _boostTimer -= Time.deltaTime;
                if (_boostTimer <= 0f)
                {
                    _boostCoefficient = 1f;
                    _boostTimer = 0f;
                }
            }

            // Handle cooldown timer
            if (_boostCooldownTimer > 0f)
            {
                _boostCooldownTimer -= Time.deltaTime;
                if (_boostCooldownTimer <= 0f)
                {
                    _isBoostCooldowning = false;
                    _boostCooldownTimer = 0f;
                }
            }
        }
    }
}