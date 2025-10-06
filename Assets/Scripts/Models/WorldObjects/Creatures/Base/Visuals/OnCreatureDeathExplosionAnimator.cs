using Models.WorldObjects.Creatures.Base.Visuals.Pools;
using UnityEngine;
using Zenject;

namespace Models.WorldObjects.Creatures.Base.Visuals
{
    /// <summary>
    /// Animates an explosion effect when the creature dies.
    /// Leaves a trail for some time after death.
    /// </summary>
    public class OnCreatureDeathExplosionAnimator : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Creature _creature;
        
        private OnDeathExplosionEffectsPool _onDeathExplosionEffectsPool;
        
        [Inject]
        private void Construct(OnDeathExplosionEffectsPool onDeathExplosionEffectsPool)
        {
            _onDeathExplosionEffectsPool = onDeathExplosionEffectsPool;
        }

        private void OnEnable()
        {
            _creature.OnDestroyed += AnimateExplosion;
        }
        
        private void OnDisable()
        {
            _creature.OnDestroyed -= AnimateExplosion;
        }

        private void AnimateExplosion()
        {
            var explosion = _onDeathExplosionEffectsPool.GetFreeObject();
            explosion.transform.position = transform.position;
            explosion.Play();
        }
    }
}