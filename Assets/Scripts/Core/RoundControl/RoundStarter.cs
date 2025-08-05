using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UserInterface.Functional;

namespace Core.RoundControl
{
    public class RoundStarter : MonoBehaviour
    {
        [SerializeField] private SelectableItemsMenu selectableItemsMenu;
        [SerializeField] private List<SelectableItemSO> playerWeapons;

        private void Start()
        {
            selectableItemsMenu.ShowMenuToSelect(playerWeapons);
        }
    }
}