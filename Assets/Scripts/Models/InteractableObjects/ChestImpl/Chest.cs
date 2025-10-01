using System;
using System.Collections;
using Models.InteractableObjects.Base;
using Models.Items.Base.Enums;
using Models.Items.Base.Spawners;
using UnityEngine;
using Zenject;

namespace Models.InteractableObjects.ChestImpl
{
    public class Chest : InteractableObject
    {
        [Header("Settings")]
        [SerializeField] private ItemsLoadType _itemsRarityTypeInsideTheChest;
        [SerializeField] private float _delayBeforeReturnToPool = 5f;
        
        private ItemsSpawner _itemsSpawner;
        
        public float DelayBeforeReturnToPool => _delayBeforeReturnToPool;
        public event Action OnChestOpened;
        
        [Inject]
        private void Construct(ItemsSpawner itemsSpawner)
        {
            _itemsSpawner = itemsSpawner;
        }
        
        protected override void OnPlayerInteracting()
        {
            OnChestOpened?.Invoke();
            _onCanInteractHintText.HideHint();
            _itemsSpawner.RunArtefactSelectionProcess(_itemsRarityTypeInsideTheChest);
            StartCoroutine(ReturnToPoolCoroutine());
        }

        private IEnumerator ReturnToPoolCoroutine()
        {
            yield return new WaitForSeconds(_delayBeforeReturnToPool);
            ReturnToPool();
        }
    }
}