using System.Collections.Generic;
using Models.Items.Base.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Animators;
using UserInterface.Animators.Enums;
using Zenject;

namespace UserInterface.Functional
{
    public class SelectableItemsMenu : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private Transform buttonContainer;
        [SerializeField] private Transform targetParent;
        [SerializeField] private List<SelectableItemCard> cards;
        
        private DiContainer _diContainer;
        private ScreenBorderAnimator _screenBorderAnimator;
        
        [Inject]
        private void Construct(DiContainer container, ScreenBorderAnimator screenBorderAnimator)
        {
            _diContainer = container;
            _screenBorderAnimator = screenBorderAnimator;
        }

        public void ShowMenuToSelect(List<SelectableItemSO> itemsData)
        {
            gameObject.SetActive(true);
            _screenBorderAnimator.SetBorder(ScreenBorderType.ItemSelectMenuBorder);
            
            foreach (var itemData in itemsData)
            {
                var currentCard = cards[itemsData.IndexOf(itemData)]; 
                
                currentCard.SetData(itemData);
                currentCard.AddOnClickAction(()=>OnItemSelected(itemData.prefabToSpawn));
            }
        }

        private void OnItemSelected(GameObject itemToSpawn)
        {
            SpawnItem(itemToSpawn);

            _screenBorderAnimator.HideBorder();
            gameObject.SetActive(false);
        }

        private void SpawnItem(GameObject itemToSpawn)
        {
            if (itemToSpawn != null)
            {
                GameObject instance = _diContainer.InstantiatePrefab(itemToSpawn, targetParent);
                instance.transform.localPosition = Vector3.zero;
            }
        }
    }
}