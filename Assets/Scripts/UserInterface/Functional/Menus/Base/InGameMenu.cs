using Core.GameControl;
using Core.GameControl.Enums;
using UnityEngine;
using UserInterface.Animators;
using UserInterface.Animators.Enums;
using Zenject;

namespace UserInterface.Functional.Menus.Base
{
    // Helper class for in-game menus
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private GamePauser _gamePauser;
        
        private ScreenBorderAnimator _screenBorderAnimator;
        private bool _isNeedToHideBorder;
        
        protected bool IsMenuCanBeOpened => GameStateHolder.IsPlayerInSomeMenu() == false;
        
        [Inject]
        private void Construct(ScreenBorderAnimator screenBorderAnimator)
        {
            _screenBorderAnimator = screenBorderAnimator;
        }
        
        protected void OpenMenu(ScreenBorderType borderType)
        {
            if(!IsMenuCanBeOpened) return;
            
            SetActiveChildes(this.gameObject, true);
            
            GameStateHolder.UpdateScreenState(ScreenState.InSomeMenu);
            _gamePauser.PauseGame();

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
            
            GameStateHolder.UpdateScreenState(ScreenState.InGame);
            
            if (resumeGame)
                _gamePauser.UnpauseGame();
            
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