using UnityEngine;
using UnityEngine.Serialization;

namespace Models.WorldObjects.Creatures.Base.Animating
{
    /// <summary>
    /// Animates damage if creature`s damage trigger is "IsDamaged".
    /// </summary>
    public class OnCreatureDamagedAnimator : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Animator _animator;
        [FormerlySerializedAs("_creatureView")] [SerializeField] private Creature _creature;
        
        private readonly int _damageTriggerHash = Animator.StringToHash("IsDamaged");

        private void OnEnable()
        {
            _creature.Health.OnDamaged += AnimateDamage;
        }
        
        private void OnDisable()
        {
            _creature.Health.OnDamaged  -= AnimateDamage;
        }

        private void AnimateDamage()
        {
            _animator.SetTrigger(_damageTriggerHash);
        }
    }
}