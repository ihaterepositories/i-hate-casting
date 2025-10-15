using Core.Input.Interfaces;
using Models.WorldObjects.Creatures.Base.Moving.Base;
using Models.WorldObjects.Creatures.Base.Moving.Interfaces;
using Models.WorldObjects.Creatures.Base.StatsHandling;
using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.Moving
{
    public class ByInputMover : Mover, IMoveService
    {
        private readonly Rigidbody2D _rb;
        private readonly CreatureStatsCalculator _statsCalculator;
        private readonly IInputHandler _inputHandler;
        
        private float _horizontalAxis;
        private float _verticalAxis;
        
        public ByInputMover(
            Rigidbody2D rigidbody2D, 
            CreatureStatsCalculator statsCalculator,
            IInputHandler inputHandler)
        {
            _rb = rigidbody2D;
            _statsCalculator = statsCalculator;
            _inputHandler = inputHandler;
        }
        
        public void Move()
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