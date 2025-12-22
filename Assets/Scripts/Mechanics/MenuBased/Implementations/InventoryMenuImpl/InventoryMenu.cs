using System.Collections.Generic;
using Mechanics.ItemSelecting.ScriptableObjects;
using Mechanics.MenuBased.Base;
using Mechanics.MenuBased.Implementations.SelectionMenuImpl;
using Mechanics.MenuBased.Models;
using UnityEngine;

namespace Mechanics.MenuBased.Implementations.InventoryMenuImpl
{
    // TODO: Add functionality to handle situation when item removed from player
    public class InventoryMenu : Menu
    {
        [SerializeField] private List<SelectableItemCard> _itemCards;
        
        private bool _isOpened;
        private readonly List<SelectableItemSo> _activeItemsData = new();
        
        private void OnEnable()
        {
            SelectionMenu.OnItemSelected += AddActiveItemData;
        }

        private void OnDisable()
        {
            SelectionMenu.OnItemSelected -= AddActiveItemData;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            {
                ToggleMenu();
            }
        }
        
        private void AddActiveItemData(SelectableItemSo itemData)
        {
            _activeItemsData.Add(itemData);
        }

        private void HandleLastItemDestroy()
        {
            _activeItemsData.RemoveAt(_activeItemsData.Count - 1);
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
                OpenMenu();
                _isOpened = true;

                ShowActiveItems();
            }
        }

        private void ShowActiveItems()
        {
            for (int i = 0; i < _activeItemsData.Count; i++)
            {
                if (i >= _itemCards.Count)
                {
                    Debug.LogWarning("Not enough item cards to display all active items.");
                    break;
                }
                
                _itemCards[i].Initialize(_activeItemsData[i]);
                _itemCards[i].AnimateFlipUp();
            }
        }
    }
}