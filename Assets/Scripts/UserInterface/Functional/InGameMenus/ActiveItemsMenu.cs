using System;
using System.Collections.Generic;
using Models.Items.Base.ScriptableObjects;
using Models.Items.Spawners.Base;
using UnityEngine;
using UserInterface.Functional.InGameMenus.Base;

namespace UserInterface.Functional.InGameMenus
{
    public class ActiveItemsMenu : InGameMenu
    {
        [SerializeField] private List<SelectableItemCard> _itemCards;
        private bool _isOpened;
        private int _lastFilledCardIndex = -1;

        private void OnEnable()
        {
            ItemSpawner.OnItemSelectedInMenu += FillActiveItemCard;
            ItemSpawner.OnLastSpawnedItemDestroyed += HandleLastItemDestroy;
        }

        private void OnDisable()
        {
            ItemSpawner.OnItemSelectedInMenu -= FillActiveItemCard;
            ItemSpawner.OnLastSpawnedItemDestroyed -= HandleLastItemDestroy;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            {
                ToggleMenu();
            }
        }
        
        private void ToggleMenu()
        {
            if (_isOpened)
            {
                CloseMenu();
                
                foreach (var itemCard in _itemCards)
                {
                    itemCard.gameObject.SetActive(false);
                }
                
                _isOpened = false;
            }
            else
            {
                if(!IsMenuCanBeOpened) return;
                OpenMenu();

                foreach (var itemCard in _itemCards)
                {
                    itemCard.gameObject.SetActive(true);
                }
                
                _isOpened = true;
            }
        }

        private void FillActiveItemCard(SelectableItemSo itemData)
        {
            _lastFilledCardIndex += 1;
            _itemCards[_lastFilledCardIndex].SetData(itemData);
        }

        private void HandleLastItemDestroy()
        {
            _itemCards[_lastFilledCardIndex].ClearData();
            _lastFilledCardIndex -= 1;
        }
    }
}