using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.Functional
{
    public class SelectableItemsMenu : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private Transform buttonContainer;
        [SerializeField] private Transform targetParent;
        [SerializeField] private List<Button> selectableButtons;
        
        private DiContainer _diContainer;
        
        [Inject]
        private void Construct(DiContainer container)
        {
            _diContainer = container;
        }

        public void ShowMenuToSelect(List<SelectableItemSO> items)
        {
            gameObject.SetActive(true);
            
            foreach (var item in items)
            {
                var currentButton = selectableButtons[items.IndexOf(item)]; 
                
                currentButton.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
                currentButton.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.itemName;
                currentButton.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = item.description;

                currentButton.onClick.AddListener(() => OnItemSelected(item.prefabToSpawn));
            }
        }

        private void OnItemSelected(GameObject itemToSpawn)
        {
            if (itemToSpawn != null)
            {
                GameObject instance = _diContainer.InstantiatePrefab(itemToSpawn, targetParent);
                instance.transform.localPosition = Vector3.zero;
            }

            gameObject.SetActive(false);
        }
    }
}