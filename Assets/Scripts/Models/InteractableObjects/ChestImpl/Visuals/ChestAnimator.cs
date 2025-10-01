using System.Collections;
using DG.Tweening;
using Models.InteractableObjects.ChestImpl.ScriptableObjects;
using UnityEngine;

namespace Models.InteractableObjects.ChestImpl.Visuals
{
    public class ChestAnimator : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Chest _chest;
        [SerializeField] private SpriteRenderer _chestSpriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private ChestSpritesSo _chestSpritesSo;

        private void OnEnable()
        {
            _chest.OnChestOpened += AnimateOpening;
        }
        
        private void OnDisable()
        {
            _chest.OnChestOpened -= AnimateOpening;
            
            _chestSpriteRenderer.DOKill();
        }

        private void AnimateOpening()
        {
            _animator.enabled = false;
            _chestSpriteRenderer.sprite = _chestSpritesSo.OpenedChestSprite;
            StartCoroutine(DisappearanceAnimationCoroutine());
        }
        
        private IEnumerator DisappearanceAnimationCoroutine()
        {
            yield return new WaitForSeconds(_chest.DelayBeforeReturnToPool - 1f);
            _chestSpriteRenderer.DOFade(0f, 0.5f).OnComplete(() =>
            {
                _chestSpriteRenderer.sprite = _chestSpritesSo.DefaultChestSprite;
                _animator.enabled = true;
            });
        }
    }
}