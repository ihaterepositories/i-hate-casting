using System;
using System.Collections.Generic;
using Mechanics.MenuBased.Selection;
using Models.Items.Base.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Models.Items.Base.Spawners
{
    public abstract class ItemsSpawner : MonoBehaviour
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
        
        protected void ShowSelectionFor(List<SelectableItemSo> items)
        {
            if (_selectionMenu == null)
            {
                Debug.LogError("SelectableItemsMenu is not assigned in the inspector.");
                return;
            }

            if (items == null || items.Count == 0 || items.Count > 3)
            {
                Debug.LogError("List is empty or not assigned or too big.");
                return;
            }

            _selectionMenu.ShowMenuToSelect(items, SpawnItem);
        }

        private void SpawnItem(SelectableItemSo selectableItemSo)
        {
            OnItemSelectedInMenu?.Invoke(selectableItemSo);
            
            if (selectableItemSo.PrefabToSpawn != null)
            {
                GameObject prefab = _diContainer.InstantiatePrefab(selectableItemSo.PrefabToSpawn, _targetParent);
                prefab.transform.localPosition = Vector3.zero;
                OnItemSpawned?.Invoke(prefab);
                _lastSpawnedItem = prefab;
            }
            else
            {
                Debug.LogError($"Prefab to spawn is null for item: {selectableItemSo.name}");
            }
        }
    }
}