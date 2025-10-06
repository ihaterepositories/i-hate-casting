using UnityEngine;

namespace Models.WorldObjects.Creatures.Base.Visuals
{
    /// <summary>
    /// Animates damage if creature`s damage trigger is "IsDamaged".
    /// </summary>
    public class OnCreatureDamagedAnimator : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Creature _creature;
        
        private readonly int _damageTriggerHash = Animator.StringToHash("IsDamaged");

        private void OnEnable()
        {
            _creature.OnDamaged += AnimateDamage;
        }
        
        private void OnDisable()
        {
            _creature.OnDamaged -= AnimateDamage;
        }

        private void AnimateDamage()
        {
            _animator.SetTrigger(_damageTriggerHash);
        }
    }
}