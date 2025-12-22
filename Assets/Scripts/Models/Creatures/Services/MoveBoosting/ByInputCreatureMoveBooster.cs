using System;
using Core.Input.Interfaces;
using Models.Creatures.Services.MoveBoosting.Interfaces;
using Models.Creatures.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.MoveBoosting
{
    // TODO: refactor timers with asycnhronous tasks
    public class ByInputCreatureMoveBooster : ICreatureMoveBooster
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly ICreatureStatsCalculator _statsCalculateService;
        private readonly IInputHandler _inputHandler;
        
        private float _boostCoefficient = 1f;
        private bool _isBoostCooldowning;
        
        private float _boostTimer;
        private float _boostCooldownTimer;
        
        private bool _isMoving => _rigidbody2D.velocity.sqrMagnitude > 0.01f;
        
        public event Action OnBoostActivated;
        
        public ByInputCreatureMoveBooster (
            Rigidbody2D rigidbody2D, 
            ICreatureStatsCalculator statsCalculateService,
            IInputHandler inputHandler)
        {
            _rigidbody2D = rigidbody2D;
            _statsCalculateService = statsCalculateService;
            _inputHandler = inputHandler;
        }

        public float BoostCooldownDuration => _statsCalculateService.CalculateBoostCooldownTime();
        public float BoostCooldownTimeElapsed => _statsCalculateService.CalculateBoostCooldownTime() - _boostCooldownTimer;

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
                OnBoostActivated?.Invoke();
            
                _boostTimer = _statsCalculateService.CalculateBoostDuration();
                _boostCooldownTimer = _statsCalculateService.CalculateBoostCooldownTime();
            
                _boostCoefficient = _statsCalculateService.CalculateBoostStrength();
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