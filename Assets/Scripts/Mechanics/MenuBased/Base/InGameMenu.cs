using Core.GameControl;
using Core.GameControl.Enums;
using Core.Pausing.Interfaces;
using UnityEngine;
using Zenject;

namespace Mechanics.MenuBased.Base
{
    /// <summary>
    /// Helper class for all during game round menus.
    /// Provides functionality to open/close the menu.
    /// </summary>
    public class Menu : MonoBehaviour
    {
        private IPauser _pauser;
        private bool IsMenuCanBeOpened => ScreenStateHolder.IsPlayerInSomeMenu() == false;
        
        [Inject]
        private void Construct(IPauser pauser)
        {
            _pauser = pauser;
        }
        
        protected void OpenMenu()
        {
            if(!IsMenuCanBeOpened) return;
            
            SetActiveChildes(transform, true);
            
            ScreenStateHolder.UpdateScreenState(ScreenState.InSomeMenu);
            _pauser.Pause();
        }
        
        protected void CloseMenu(bool resumeGame = true)
        {
            SetActiveChildes(transform, false);
            
            ScreenStateHolder.UpdateScreenState(ScreenState.InGame);
            
            if (resumeGame)
                _pauser.Resume();
        }
        
        // Recursively sets active state for all childes of the given parent object.
        // Used to open/close the menu.
        private void SetActiveChildes(Transform parentTransform, bool isActive)
        {
            foreach (Transform child in parentTransform)
            {
                child.gameObject.SetActive(isActive);
                SetActiveChildes(child, isActive);
            }
        }
    }
}