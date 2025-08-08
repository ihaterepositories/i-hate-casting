using System;
using System.Collections.Generic;
using Models.Items.Base.ScriptableObjects;
using UnityEngine;
using UserInterface.Functional;
using Zenject;

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