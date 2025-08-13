using System;
using System.Collections.Generic;
using Models.Items.Base.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UserInterface.Animators;
using UserInterface.Animators.Enums;
using UserInterface.Functional.InGameMenus.Base;
using Zenject;

namespace UserInterface.Functional.InGameMenus
{
    public class SelectionMenu : InGameMenu
    {
        [FormerlySerializedAs("buttonPrefab")] [SerializeField] private GameObject _buttonPrefab;
        [FormerlySerializedAs("buttonContainer")] [SerializeField] private Transform _buttonContainer;
        [FormerlySerializedAs("cards")] [SerializeField] private List<SelectableItemCard> _cards;
        
        public void ShowMenuToSelect(List<SelectableItemSo> itemsData, Action<SelectableItemSo> onSelectedCallback = null)
        {
            if(!IsMenuCanBeOpened) return;
            
            OpenMenu();
            
            foreach (var itemData in itemsData)
            {
                var currentCard = _cards[itemsData.IndexOf(itemData)]; 
                
                currentCard.SetData(itemData);
                currentCard.AddOnClickAction(() =>
                {
                    onSelectedCallback?.Invoke(itemData);
                    CloseMenu();
                });
            }
        }
    }
}