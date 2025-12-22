using Models.Creatures.Services.Animating.Interfaces;
using Models.Creatures.Services.Living.Interfaces;
using Models.Creatures.Services.Moving.Interfaces;
using UnityEngine;

namespace Models.Creatures.Services.Animating
{
    public class CreatureAnimationLauncher : ICreatureAnimationLauncher
    {
        private readonly Animator _animator;
        
        private readonly ICreatureMover _moveService;
        private readonly ICreatureHealth _healthService;

        private bool _wasAnimatingRun;
        
        private readonly int _runTriggerHash = Animator.StringToHash("isRunning");
        private readonly int _damageTriggerHash = Animator.StringToHash("isDamaged");

        public CreatureAnimationLauncher(
            Animator animator, 
            AnimatorOverrideController animatorOverrideController,
            ICreatureMover moveService,
            ICreatureHealth healthService)
        {
            _animator = animator;
            _moveService = moveService;
            _healthService = healthService;
            
            _animator.runtimeAnimatorController = animatorOverrideController;
        }
        
        public void Dispose()
        {
            _healthService.OnDamaged -= AnimateDamage;
        }

        public void InitializeTriggerAnimations()
        {
            _healthService.OnDamaged += AnimateDamage;
        }
        
        // _wasAnimatingRun - special flag to prevent _animator.SetBool every frame
        public void AnimateMoving()
        {
            if (_moveService.IsMoving && !_wasAnimatingRun)
            {
                _animator.SetBool(_runTriggerHash, true);
                _wasAnimatingRun = true;
            }
            else if (!_moveService.IsMoving && _wasAnimatingRun)
            {
                _animator.SetBool(_runTriggerHash, false);
                _wasAnimatingRun = false;
            }
        }

        private void AnimateDamage()
        {
            _animator.SetTrigger(_damageTriggerHash);
        }
    }
}