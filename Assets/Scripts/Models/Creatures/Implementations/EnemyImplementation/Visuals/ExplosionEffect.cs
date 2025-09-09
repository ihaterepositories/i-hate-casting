using System;
using System.Collections;
using DG.Tweening;
using Models.Pooling;
using UnityEngine;

namespace Models.Creatures.Implementations.EnemyImplementation.Visuals
{
    public class ExplosionEffect : PoolAbleMonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        
        private readonly int _animationTriggerHash = Animator.StringToHash("DoExplosion");
        private Color _defaultVisibleColor;

        private void Awake()
        {
            _defaultVisibleColor = _spriteRenderer.color;
            _defaultVisibleColor.a = 1f;
        }

        public void Play()
        {
            _spriteRenderer.color = _defaultVisibleColor;
            _animator.SetTrigger(_animationTriggerHash);
            StartCoroutine(RemoveAfterAnimation());
        }
        
        private IEnumerator RemoveAfterAnimation()
        {
            yield return new WaitForSeconds(10f);
            _spriteRenderer.DOFade(0f, 1f).OnComplete(ReturnToPool);
        }
    }
}