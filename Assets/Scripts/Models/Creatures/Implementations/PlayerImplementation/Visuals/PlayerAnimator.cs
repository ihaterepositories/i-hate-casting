using Core.GameControl;
using Core.Input.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Models.Creatures.Implementations.PlayerImplementation.Visuals
{
    public class PlayerAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("animator")] [SerializeField] private Animator _animator;
        
        private IInputHandler _inputHandler;
        private float _horizontalAxis;
        private float _verticalAxis;
        
        private readonly int _isRunningParameter = Animator.StringToHash("isRunning");

        [Inject]
        private void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        private void Update()
        {
            if (GameStateHolder.IsGamePaused) return;
            
            SetAxisValues();
            AnimateRun();
        }
        
        private void SetAxisValues()
        {
            _horizontalAxis = _inputHandler.GetHorizontalAxisValue();
            _verticalAxis = _inputHandler.GetVerticalAxisValue();
        }
        
        private void AnimateRun()
        {
            if (_horizontalAxis != 0 || _verticalAxis != 0)
                _animator.SetBool(_isRunningParameter, true);
            else
                _animator.SetBool(_isRunningParameter, false);
        }
    }
}