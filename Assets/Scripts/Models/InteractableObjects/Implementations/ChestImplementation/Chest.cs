using Models.Creatures.Implementations.PlayerImplementation;
using Models.Creatures.Items.Implementations.Artefacts.Base.Enums;
using Models.Creatures.Items.Implementations.Artefacts.Base.Spawners;
using Models.InteractableObjects.Base;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Models.InteractableObjects.Implementations.ChestImplementation
{
    public abstract class Chest : InteractableObject
    {
        [SerializeField] private ArtefactsLoadType _artefactsRarityTypeInsideTheChest;
        
        private ArtefactsSpawner _artefactsSpawner;
        private bool _isOpened;
        
        [Inject]
        private void Construct(ArtefactsSpawner artefactsSpawner)
        {
            _artefactsSpawner = artefactsSpawner;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                if (_isOpened)
                    ReturnToPool();
            }
        }
        
        protected override void OnPlayerInteracting()
        {
            _isOpened = true;
            _artefactsSpawner.RunArtefactSelectionProcess(_artefactsRarityTypeInsideTheChest);
        }
    }
}