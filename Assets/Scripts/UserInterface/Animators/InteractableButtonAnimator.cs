using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UserInterface.Animators
{
    public class InteractableButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [FormerlySerializedAs("button")] [SerializeField] private Button _button;

        private Vector3 _defaultScale;
        
        private void Start()
        {
            _defaultScale = _button.transform.localScale;
        }

        private void OnDisable()
        {
            _button.DOKill();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _button.transform.DOScale(_defaultScale * 1.1f, 0.2f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _button.transform.DOScale(_defaultScale, 0.2f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);
        }
    }
}