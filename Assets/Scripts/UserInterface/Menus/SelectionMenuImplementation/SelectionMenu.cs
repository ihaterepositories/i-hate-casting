using System;
using System.Collections.Generic;
using Models.Items.Base.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UserInterface.GameScreenAnimations.ExtraBorder.Enums;
using UserInterface.Menus.Base;
using UserInterface.Menus.Base.Models;

namespace UserInterface.Menus.SelectionMenuImplementation
{
    public class SelectionMenu : InGameMenu
    {
        [FormerlySerializedAs("buttonPrefab")] [SerializeField] private GameObject _buttonPrefab;
        [FormerlySerializedAs("buttonContainer")] [SerializeField] private Transform _buttonContainer;
        [SerializeField] private Image _cardsClickBlockerImage; 
        [FormerlySerializedAs("cards")] [SerializeField] private List<SelectableItemCard> _cards;

        public static event Action<SelectableItemSo> OnItemSelected;
        
        public void OpenMenuToSelect(List<SelectableItemSo> itemsData, Action<SelectableItemSo> onSelectedCallback = null)
        {
            if (itemsData == null || itemsData.Count == 0 || itemsData.Count > _cards.Count)
            {
                Debug.LogError("Items list is empty or not assigned or too big.");
                return;
            }
            
            OpenMenu(ScreenBorderType.ItemSelectMenuBorder);
            
            _cardsClickBlockerImage.gameObject.SetActive(true);

            int i = 0;
            foreach (var itemData in itemsData)
            {
                var currentCard = _cards[i]; 
                
                currentCard.Initialize(itemData, () =>
                {
                    onSelectedCallback?.Invoke(itemData);
                    OnItemSelected?.Invoke(itemData);
                    CloseMenu();
                });

                i++;
            }

            FlipUpCardsSequenced(_cards.Count-1);
        }
        
        private void FlipUpCardsSequenced(int lastCardToAnimateInSequenceIndex)
        {
            if (lastCardToAnimateInSequenceIndex < 0)
            {
                _cardsClickBlockerImage.gameObject.SetActive(false);
                return;
            }
            
            _cards[lastCardToAnimateInSequenceIndex].AnimateFlipUp(()=>
                FlipUpCardsSequenced(lastCardToAnimateInSequenceIndex-1));
        }
    }
}