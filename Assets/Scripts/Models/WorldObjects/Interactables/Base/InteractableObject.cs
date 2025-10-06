using Core.Input.Interfaces;
using Models.WorldObjects.Base.Pooling;
using Models.WorldObjects.Creatures.PlayerImpl;
using Models.WorldObjects.Interactables.Base.Visuals;
using UnityEngine;
using Zenject;

namespace Models.WorldObjects.Interactables.Base
{
    /// <summary>
    /// Base class for all objects that the player can interact with by pressing a specific button.
    /// Provides the text hint when the player is in interacting range.
    /// Interacting range is determined by a trigger collider.
    /// 
    /// OnPlayerInteracting() is called once when the player presses the interaction button while in range.
    /// </summary>
    public abstract class InteractableObject : PoolableMonoBehaviour
    {
        private IInputHandler _inputHandler;
        protected OnCanInteractHintText _onCanInteractHintText;
        
        private bool _isPlayerInInteractionRange;
        private bool _isInteracted;
        
        [Inject]
        private void Construct(IInputHandler inputHandler, OnCanInteractHintText onCanInteractHintText)
        {
            _inputHandler = inputHandler;
            _onCanInteractHintText = onCanInteractHintText;
        }

        private void Update()
        {
            if (_inputHandler.IsInteractingButtonPressed() && 
                _isPlayerInInteractionRange &&
                !_isInteracted)
            {
                _isInteracted = true;
                OnPlayerInteracting();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isInteracted) return;
            if (!other.TryGetComponent<Player>(out var player)) return;
            
            _onCanInteractHintText.ShowHint();
            _isPlayerInInteractionRange = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_isInteracted) return;
            if (!other.TryGetComponent<Player>(out var player)) return;
            
            _onCanInteractHintText.HideHint();
        }

        public override void OnTakenFromPool()
        {
            _isInteracted = false;
            _isPlayerInInteractionRange = false;
        }

        protected abstract void OnPlayerInteracting();
    }
}