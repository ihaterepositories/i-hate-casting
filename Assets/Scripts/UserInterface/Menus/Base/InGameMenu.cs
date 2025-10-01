using Core.GameControl;
using Core.GameControl.Enums;
using UnityEngine;
using Zenject;

namespace UserInterface.Menus.Base
{
    // Helper class for in-game menus
    public class InGameMenu : MonoBehaviour
    {
        private GamePauser _gamePauser;
        private bool IsMenuCanBeOpened => ScreenStateHolder.IsPlayerInSomeMenu() == false;
        
        [Inject]
        private void Construct(GamePauser gamePauser)
        {
            _gamePauser = gamePauser;
        }
        
        protected void OpenMenu()
        {
            if(!IsMenuCanBeOpened) return;
            
            SetActiveChildes(this.gameObject, true);
            
            ScreenStateHolder.UpdateScreenState(ScreenState.InSomeMenu);
            _gamePauser.PauseGame();
        }
        
        protected void CloseMenu(bool resumeGame = true)
        {
            SetActiveChildes(this.gameObject, false);
            
            ScreenStateHolder.UpdateScreenState(ScreenState.InGame);
            
            if (resumeGame)
                _gamePauser.UnpauseGame();
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