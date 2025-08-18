using System;
using System.Collections.Generic;
using Mechanics.MenuBased.Base;
using Mechanics.MenuBased.Base.Models;
using Models.Items.Base.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UserInterfaceUtils.Animators.Enums;

namespace Mechanics.MenuBased.Selection
{
    public class SelectionMenu : InGameMenu
    {
        [FormerlySerializedAs("buttonPrefab")] [SerializeField] private GameObject _buttonPrefab;
        [FormerlySerializedAs("buttonContainer")] [SerializeField] private Transform _buttonContainer;
        [FormerlySerializedAs("cards")] [SerializeField] private List<SelectableItemCard> _cards;

        public void ShowMenuToSelect(List<SelectableItemSo> itemsData, Action<SelectableItemSo> onSelectedCallback = null)
        {
            if (itemsData == null || itemsData.Count == 0 || itemsData.Count > _cards.Count)
            {
                Debug.LogError("Items list is empty or not assigned or too big.");
                return;
            }
            
            OpenMenu(ScreenBorderType.ItemSelectMenuBorder);

            int i = 0;
            foreach (var itemData in itemsData)
            {
                var currentCard = _cards[i]; 
                
                currentCard.FillWith(itemData);
                currentCard.AddOnClickAction(() =>
                {
                    onSelectedCallback?.Invoke(itemData);
                    CloseMenu();
                });

                i++;
            }
        }
    }
}