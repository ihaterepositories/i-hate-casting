using System;
using System.Collections.Generic;
using Models.Items.Base.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UserInterface.Functional;
using Zenject;

namespace Core.ItemSpawners.Base
{
    public abstract class ItemSpawner : MonoBehaviour
    {
        [FormerlySerializedAs("selectionMenu")] [SerializeField] private SelectionMenu _selectionMenu;
        [FormerlySerializedAs("targetParent")] [SerializeField] private Transform _targetParent;
        
        private DiContainer _diContainer;
        
        public event Action<GameObject> OnSpawned;
        public event Action<SelectableItemSo> OnSelected; 
        
        [Inject]
        private void Construct(DiContainer container)
        {
            _diContainer = container;
        }
        
        protected void ShowSelectionFor(List<SelectableItemSo> prefabs)
        {
            if (_selectionMenu == null)
            {
                Debug.LogError("SelectableItemsMenu is not assigned in the inspector.");
                return;
            }

            if (prefabs == null || prefabs.Count == 0)
            {
                Debug.LogError("Base weapon prefabs list is empty or not assigned.");
                return;
            }

            _selectionMenu.ShowMenuToSelect(prefabs, OnItemSelected);
        }

        private void OnItemSelected(SelectableItemSo item)
        {
            OnSelected?.Invoke(item);
            SpawnItem(item._prefabToSpawn);
        }
        
        private void SpawnItem(GameObject itemToSpawn)
        {
            if (itemToSpawn != null)
            {
                GameObject instance = _diContainer.InstantiatePrefab(itemToSpawn, _targetParent);
                instance.transform.localPosition = Vector3.zero;
                OnSpawned?.Invoke(instance);
            }
        }
    }
}