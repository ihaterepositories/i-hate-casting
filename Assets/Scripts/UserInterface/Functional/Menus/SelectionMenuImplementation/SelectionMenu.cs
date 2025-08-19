using System;
using System.Collections.Generic;
using Models.Items.Base.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UserInterface.Animators.Enums;
using UserInterface.Functional.Menus.Base;
using UserInterface.Functional.Menus.Base.Models;

namespace UserInterface.Functional.Menus.SelectionMenuImplementation
{
    public class SelectionMenu : InGameMenu
    {
        [FormerlySerializedAs("buttonPrefab")] [SerializeField] private GameObject _buttonPrefab;
        [FormerlySerializedAs("buttonContainer")] [SerializeField] private Transform _buttonContainer;
        [FormerlySerializedAs("cards")] [SerializeField] private List<SelectableItemCard> _cards;

        public void OpenMenuToSelect(List<SelectableItemSo> itemsData, Action<SelectableItemSo> onSelectedCallback = null)
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