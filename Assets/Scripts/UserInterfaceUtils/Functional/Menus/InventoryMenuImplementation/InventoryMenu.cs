using System.Collections.Generic;
using Models.Items.Base.ScriptableObjects;
using Models.Items.Base.Spawners;
using UnityEngine;
using UserInterfaceUtils.Animators.Enums;
using UserInterfaceUtils.Functional.Menus.Base;
using UserInterfaceUtils.Functional.Menus.Base.Models;

namespace UserInterfaceUtils.Functional.Menus.InventoryMenuImplementation
{
    public class InventoryMenu : InGameMenu
    {
        [SerializeField] private List<SelectableItemCard> _itemCards;
        
        private bool _isOpened;
        private int _lastFilledCardIndex = -1;
        
        private void OnEnable()
        {
            ItemsSpawner.OnItemSelectedInMenu += FillActiveItemCard;
            ItemsSpawner.OnLastSpawnedItemDestroyed += HandleLastItemDestroy;
        }

        private void OnDisable()
        {
            ItemsSpawner.OnItemSelectedInMenu -= FillActiveItemCard;
            ItemsSpawner.OnLastSpawnedItemDestroyed -= HandleLastItemDestroy;
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
                _isOpened = false;
            }
            else
            {
                OpenMenu(ScreenBorderType.ItemSelectMenuBorder);
                _isOpened = true;
            }
        }

        private void FillActiveItemCard(SelectableItemSo itemData)
        {
            _lastFilledCardIndex += 1;
            _itemCards[_lastFilledCardIndex].FillWith(itemData);
        }

        private void HandleLastItemDestroy()
        {
            _itemCards[_lastFilledCardIndex].ClearData();
            _lastFilledCardIndex -= 1;
        }
    }
}