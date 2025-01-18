using Core.Input.Interfaces;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Player.Animating
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
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
                animator.SetBool(_isRunningParameter, true);
            else
                animator.SetBool(_isRunningParameter, false);
        }
    }
}