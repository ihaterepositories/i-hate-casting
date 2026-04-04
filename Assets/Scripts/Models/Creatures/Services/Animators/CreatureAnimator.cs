using Models.Creatures.Services.Animators.Interfaces;
using Models.Creatures.Services.Health.Interfaces;
using Models.Creatures.Services.Movers.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.Animators
{
    public class CreatureAnimator : ICreatureAnimator
    {
        private readonly Animator _animator;
        
        private readonly ICreatureMover _moveService;
        private readonly ICreatureHealth _healthService;

        private bool _isRunAnimationPlaying;
        
        private readonly int _runTriggerHash = Animator.StringToHash("isRunning");
        private readonly int _damageTriggerHash = Animator.StringToHash("isDamaged");

        public CreatureAnimator(
            Animator animator, 
            AnimatorOverrideController animatorOverrideController,
            ICreatureMover moveService,
            ICreatureHealth healthService)
        {
            _animator = animator;
            _moveService = moveService;
            _healthService = healthService;
            
            _animator.runtimeAnimatorController = animatorOverrideController;
            
            // Registering trigger based animations
            _healthService.OnDamaged += AnimateDamage;
        }
        
        public void CleanResources()
        {
            _healthService.OnDamaged -= AnimateDamage;
        }
        
        // The _isRunAnimationPlaying flag is created to prevent _animator.SetBool every frame.
        public void Tick()
        {
            if (_moveService.IsMoving && !_isRunAnimationPlaying)
            {
                StartRunAnimation();
            }
            else if (!_moveService.IsMoving && _isRunAnimationPlaying)
            {
                ExitRunAnimation();
            }
        }

        private void StartRunAnimation()
        {
            _animator.SetBool(_runTriggerHash, true);
            _isRunAnimationPlaying = true;
        }
        
        private void ExitRunAnimation()
        {
            _animator.SetBool(_runTriggerHash, false);
            _isRunAnimationPlaying = false;
        }

        private void AnimateDamage()
        {
            _animator.SetTrigger(_damageTriggerHash);
        }
    }
}