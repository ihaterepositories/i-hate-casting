using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UserInterface.Animators
{
    public class InteractableButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Button button;

        private Vector3 _defaultScale;
        
        private void Start()
        {
            _defaultScale = button.transform.localScale;
        }

        private void OnDisable()
        {
            button.DOKill();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            button.transform.DOScale(_defaultScale * 1.1f, 0.2f).SetEase(Ease.OutBack);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            button.transform.DOScale(_defaultScale, 0.2f).SetEase(Ease.OutBack);
        }
    }
}