using Core.Input.Interfaces;
using Models.Creatures.Implementations.PlayerImplementation;
using Models.Pooling;
using UnityEngine;
using Zenject;

namespace Models.InteractableObjects.Base
{
    public abstract class InteractableObject : PoolAbleMonoBehaviour
    {
        private IInputHandler _inputHandler;
        
        [Inject]
        private void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<Player>(out var player)) return;
            
            if (_inputHandler.IsInteractingButtonPressed())
            {
                OnPlayerInteracting();
            }
        }

        protected abstract void OnPlayerInteracting();
    }
}