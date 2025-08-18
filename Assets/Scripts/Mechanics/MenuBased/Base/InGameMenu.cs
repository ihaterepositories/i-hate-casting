using Core.GameControl;
using Core.GameControl.Enums;
using UnityEngine;
using UserInterfaceUtils.Animators;
using UserInterfaceUtils.Animators.Enums;
using Zenject;

namespace Mechanics.MenuBased.Base
{
    // Helper class for in-game menus
    public class InGameMenu : MonoBehaviour
    {
        private ScreenBorderAnimator _screenBorderAnimator;
        private bool _isNeedToHideBorder;
        
        protected bool IsMenuCanBeOpened => GameStateController.IsPlayerInSomeMenu() == false;
        
        [Inject]
        private void Construct(ScreenBorderAnimator screenBorderAnimator)
        {
            _screenBorderAnimator = screenBorderAnimator;
        }
        
        protected void OpenMenu(ScreenBorderType borderType)
        {
            if(!IsMenuCanBeOpened) return;
            
            SetActiveChildes(this.gameObject, true);
            
            GameStateController.UpdateScreenState(ScreenState.InSomeMenu);
            GameStateController.PauseGame();

            if (borderType != ScreenBorderType.None)
            {
                _screenBorderAnimator.ShowBorder(borderType);
                _isNeedToHideBorder = true;
            }
            else
            {
                _isNeedToHideBorder = false;
            }
        }
        
        protected void CloseMenu(bool resumeGame = true)
        {
            SetActiveChildes(this.gameObject, false);
            
            GameStateController.UpdateScreenState(ScreenState.InGame);
            
            if (resumeGame)
                GameStateController.UnpauseGame();
            
            if (_isNeedToHideBorder)
                _screenBorderAnimator.HideBorder();
        }
        
        void SetActiveChildes(GameObject parent, bool state)
        {
            foreach (Transform child in parent.transform)
            {
                child.gameObject.SetActive(state);
                SetActiveChildes(child.gameObject, state); // recursively setting active for all childes 
            }
        }
    }
}