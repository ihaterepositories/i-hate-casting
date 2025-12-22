using System.Collections;
using Core.Input.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Models.Interactables.Base.Visuals
{
    public class OnCanInteractHintText : MonoBehaviour
    {
        [SerializeField] private Text _hintText;
        
        private IInputHandler _inputHandler;
        
        [Inject]
        private void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        private void Awake()
        {
            _hintText.text = $"Press {_inputHandler.GetInteractingButtonName()} to interact";
            _hintText.enabled = false;
        }

        private void OnDisable()
        {
            _hintText.DOKill();
        }

        public void ShowHint()
        {
            _hintText.enabled  = true;
            _hintText.color = new Color(_hintText.color.r, _hintText.color.g, _hintText.color.b, 0f);
            _hintText.DOFade(1f, 0.25f).OnComplete((() =>
            {
                StartCoroutine(TextBlinkingCoroutine());
            }));
        }
        
        public void HideHint()
        {
            StopAllCoroutines();
            _hintText.DOFade(0f, 0.25f).OnComplete((() =>
            {
                _hintText.enabled  = false;
                _hintText.DOKill();
                _hintText.color = new Color(_hintText.color.r, _hintText.color.g, _hintText.color.b, 1f);
            }));
        }
        
        private IEnumerator TextBlinkingCoroutine()
        {
            while (true)
            {
                _hintText.DOFade(0.5f, 0.6f);
                yield return new WaitForSeconds(0.6f);
                _hintText.DOFade(1f, 0.6f);
                yield return new WaitForSeconds(0.6f);
            }
        }
    }
}