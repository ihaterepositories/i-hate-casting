using Core.GameControl;
using Core.GameControl.Enums;
using UnityEngine;

namespace UserInterface.Menus.Base
{
    // Helper class for in-game menus
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private GamePauser _gamePauser;

        private bool IsMenuCanBeOpened => GameStateHolder.IsPlayerInSomeMenu() == false;
        
        protected void OpenMenu()
        {
            if(!IsMenuCanBeOpened) return;
            
            SetActiveChildes(this.gameObject, true);
            
            GameStateHolder.UpdateScreenState(ScreenState.InSomeMenu);
            _gamePauser.PauseGame();
        }
        
        protected void CloseMenu(bool resumeGame = true)
        {
            SetActiveChildes(this.gameObject, false);
            
            GameStateHolder.UpdateScreenState(ScreenState.InGame);
            
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