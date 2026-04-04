using System;
using Core.Input.Interfaces;
using Models.Creatures.Dtos;
using Models.Creatures.Services.MoveBoosters.Interfaces;
using Models.Creatures.Services.StatsScalers.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.MoveBoosters
{
    public class ByInputCreatureMoveBooster : ICreatureMoveBooster
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly CreatureStats _stats;
        private readonly ICreatureStatsScaler _statsScaler;
        private readonly IInputHandler _inputHandler;
        
        private float _boostStrength = 1f;
        private bool _isBoostCooldowning;
        
        private float _boostTimeLeft;
        private float _boostCooldownTimeLeft;

        private float _lastBoostCooldownDurationBeforeCooldownStarted;
        
        private bool _isMoving => _rigidbody2D.linearVelocity.sqrMagnitude > 0.01f;
        
        public event Action OnBoostActivated;
        
        public ByInputCreatureMoveBooster (
            Rigidbody2D rigidbody2D, 
            CreatureStats stats,
            ICreatureStatsScaler statsScaler,
            IInputHandler inputHandler)
        {
            _rigidbody2D = rigidbody2D;
            _stats = stats;
            _statsScaler = statsScaler;
            _inputHandler = inputHandler;
        }

        public float BoostCooldownDuration => _lastBoostCooldownDurationBeforeCooldownStarted;
        public float BoostCooldownTimeElapsed => _lastBoostCooldownDurationBeforeCooldownStarted - _boostCooldownTimeLeft;

        public void Tick()
        {
            TickBoostActivation();
        }

        public void FixedTick()
        {
            TickBoost();
            TickBoostTimer();
            TickBoostCooldownTimer();
        }

        private void TickBoostActivation()
        {
            if (_inputHandler.IsBoostButtonPressed() &&
                !_isBoostCooldowning &&
                _isMoving)
            {
                OnBoostActivated?.Invoke();
            
                _boostTimeLeft = _statsScaler.ScaleBoostDuration(_stats.BoostDuration);
                _boostCooldownTimeLeft = _statsScaler.ScaleBoostCooldownTime(_stats.BoostCooldownTime);
                _lastBoostCooldownDurationBeforeCooldownStarted = _boostCooldownTimeLeft;
            
                _boostStrength = _statsScaler.ScaleBoostStrength(_stats.BoostStrength);
                _isBoostCooldowning = true;
            }
        }
        
        private void TickBoost()
        {
            _rigidbody2D.linearVelocity *= _boostStrength;
        }
        
        private void TickBoostTimer()
        {
            if (_boostTimeLeft > 0f)
            {
                _boostTimeLeft -= Time.deltaTime;
                if (_boostTimeLeft <= 0f)
                {
                    _boostStrength = 1f;
                    _boostTimeLeft = 0f;
                }
            }
        }

        private void TickBoostCooldownTimer()
        {
            if (_boostCooldownTimeLeft > 0f)
            {
                _boostCooldownTimeLeft -= Time.deltaTime;
                if (_boostCooldownTimeLeft <= 0f)
                {
                    _isBoostCooldowning = false;
                    _boostCooldownTimeLeft = 0f;
                }
            }
        }
    }
}