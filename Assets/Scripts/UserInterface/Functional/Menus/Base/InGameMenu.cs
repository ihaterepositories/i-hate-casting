using Core.GameControl;
using Core.GameControl.Enums;
using UnityEngine;
using UnityEngine.Serialization;
using UserInterface.Animators;
using UserInterface.Animators.Enums;
using Zenject;

namespace UserInterface.Functional.Menus.Base
{
    // Helper class for in-game menus
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private GamePauser _gamePauser;
        [FormerlySerializedAs("_screenBorderAnimator")] [SerializeField] private ExtraScreenBorderAnimator _extraScreenBorderAnimator;
        
        private bool _isNeedToHideBorder;

        private bool IsMenuCanBeOpened => GameStateHolder.IsPlayerInSomeMenu() == false;
        
        protected void OpenMenu(ScreenBorderType borderType)
        {
            if(!IsMenuCanBeOpened) return;
            
            SetActiveChildes(this.gameObject, true);
            
            GameStateHolder.UpdateScreenState(ScreenState.InSomeMenu);
            _gamePauser.PauseGame();

            if (borderType != ScreenBorderType.None)
            {
                _extraScreenBorderAnimator.ShowBorder(borderType);
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
                _extraScreenBorderAnimator.HideBorder();
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