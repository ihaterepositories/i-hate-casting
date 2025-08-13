using System;
using System.Collections.Generic;
using Models.Items.Base.ScriptableObjects;
using UnityEngine;
using UserInterface.Functional.InGameMenus;
using Zenject;

namespace Models.Items.Spawners.Base
{
    public abstract class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private SelectionMenu _selectionMenu;
        [SerializeField] private Transform _targetParent;
        
        private DiContainer _diContainer;
        private GameObject _lastSpawnedItem;
        
        public event Action<GameObject> OnItemSpawned;
        public static event Action<SelectableItemSo> OnItemSelectedInMenu;
        public static event Action OnLastSpawnedItemDestroyed;
        
        [Inject]
        private void Construct(DiContainer container)
        {
            _diContainer = container;
        }

        public void DestroyLastSpawnedItem()
        {
            Destroy(_lastSpawnedItem);
            OnLastSpawnedItemDestroyed?.Invoke();
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
            OnItemSelectedInMenu?.Invoke(item);
            SpawnItem(item.PrefabToSpawn);
        }
        
        private void SpawnItem(GameObject itemToSpawn)
        {
            if (itemToSpawn != null)
            {
                GameObject instance = _diContainer.InstantiatePrefab(itemToSpawn, _targetParent);
                instance.transform.localPosition = Vector3.zero;
                OnItemSpawned?.Invoke(instance);
                _lastSpawnedItem = instance;
            }
        }
    }
}