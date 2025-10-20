using Core.Input.Interfaces;
using Models.WorldObjects.Creatures.Base.Moving.Base;
using Models.WorldObjects.Creatures.Base.Moving.Interfaces;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.Moving
{
    public class ByInputMover : Mover, IMoveService
    {
        private readonly IInputHandler _inputHandler;
        
        private float _horizontalAxis;
        private float _verticalAxis;
        
        public ByInputMover (
            CreatureStatsCalculator statsCalculator, 
            Rigidbody2D rb,
            IInputHandler inputHandler) : base(statsCalculator, rb)
        {
            _inputHandler = inputHandler;
        }
        
        public void EnableMove()
        {
            // Get input and set velocity
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
            _rb.velocity = new Vector2(_horizontalAxis, _verticalAxis).normalized * _statsCalculator.GetSpeed();
        }
    }
}