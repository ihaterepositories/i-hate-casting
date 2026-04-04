using Core.Input.Interfaces;
using Models.Creatures.Dtos;
using Models.Creatures.Services.Movers.Base;
using Models.Creatures.Services.Movers.Interfaces;
using Models.Creatures.Services.StatsScalers.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.Movers
{
    public class ByInputCreatureMover : Mover, ICreatureMover
    {
        private readonly IInputHandler _inputHandler;
        
        private float _horizontalAxis;
        private float _verticalAxis;
        
        public ByInputCreatureMover (
            Rigidbody2D rb,
            CreatureStats stats,
            ICreatureStatsScaler statsScaler,
            IInputHandler inputHandler) 
            : base(rb, stats, statsScaler)
        {
            _inputHandler = inputHandler;
        }
        
        public void FixedTick()
        {
            SetDirectionByInput();
            ChangeVelocityByInput();
        }

        private void SetDirectionByInput()
        {
            _horizontalAxis = _inputHandler.GetHorizontalAxisValue();
            _verticalAxis = _inputHandler.GetVerticalAxisValue();
        }
        
        private void ChangeVelocityByInput()
        {
            _rb.linearVelocity = 
                new Vector2(_horizontalAxis, _verticalAxis).normalized * _statsScaler.ScaleSpeed(_stats.Speed);
        }
    }
}