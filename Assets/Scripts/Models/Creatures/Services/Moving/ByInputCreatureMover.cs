using Core.Input.Interfaces;
using Models.Creatures.Services.Moving.Base;
using Models.Creatures.Services.Moving.Interfaces;
using Models.Creatures.Services.StatsCalculating.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.Moving
{
    public class ByInputCreatureMover : Mover, ICreatureMover
    {
        private readonly IInputHandler _inputHandler;
        
        private float _horizontalAxis;
        private float _verticalAxis;
        
        public ByInputCreatureMover (
            ICreatureStatsCalculator statsCalculateService, 
            Rigidbody2D rb,
            IInputHandler inputHandler) : base(statsCalculateService, rb)
        {
            _inputHandler = inputHandler;
        }
        
        public void EnableMove()
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
            _rb.velocity = new Vector2(_horizontalAxis, _verticalAxis).normalized * _statsCalculateService.CalculateSpeed();
        }
    }
}