using System;
using System.Collections.Generic;
using Models.Items.Base.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UserInterface.Animators;
using UserInterface.Animators.Enums;
using Zenject;

namespace UserInterface.Functional
{
    public class SelectionMenu : MonoBehaviour
    {
        [FormerlySerializedAs("buttonPrefab")] [SerializeField] private GameObject _buttonPrefab;
        [FormerlySerializedAs("buttonContainer")] [SerializeField] private Transform _buttonContainer;
        [FormerlySerializedAs("cards")] [SerializeField] private List<SelectableItemCard> _cards;
        
        private DiContainer _diContainer;
        private ScreenBorderAnimator _screenBorderAnimator;
        
        [Inject]
        private void Construct(DiContainer container, ScreenBorderAnimator screenBorderAnimator)
        {
            _diContainer = container;
            _screenBorderAnimator = screenBorderAnimator;
        }

        public void ShowMenuToSelect(List<SelectableItemSo> itemsData, Action<SelectableItemSo> onSelectedCallback = null)
        {
            gameObject.SetActive(true);
            _screenBorderAnimator.ShowBorder(ScreenBorderType.ItemSelectMenuBorder);
            
            foreach (var itemData in itemsData)
            {
                var currentCard = _cards[itemsData.IndexOf(itemData)]; 
                
                currentCard.SetData(itemData);
                currentCard.AddOnClickAction(() =>
                {
                    _screenBorderAnimator.HideBorder();
                    gameObject.SetActive(false);
                    onSelectedCallback?.Invoke(itemData);
                });
            }
        }
    }
}