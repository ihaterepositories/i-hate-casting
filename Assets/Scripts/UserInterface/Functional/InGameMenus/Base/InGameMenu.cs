using Core.RoundControl;
using Core.RoundControl.Enums;
using UnityEngine;
using UserInterface.Animators;
using UserInterface.Animators.Enums;
using Zenject;

namespace UserInterface.Functional.InGameMenus.Base
{
    // Helper class for in-game menus
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private bool _useSetActive = true;
        [SerializeField] private bool _useExtraBorder = true;
        
        protected bool IsMenuCanBeOpened => GameStateController.IsPlayerInSomeMenu() == false;
        private ScreenBorderAnimator _screenBorderAnimator;
        
        [Inject]
        private void Construct(ScreenBorderAnimator screenBorderAnimator)
        {
            _screenBorderAnimator = screenBorderAnimator;
        }
        
        protected void OpenMenu()
        {
            // For show menu ui
            if (_useSetActive)
                gameObject.SetActive(true);
            //
            
            GameStateController.UpdateScreenState(ScreenState.InSomeMenu);
            GameStateController.PauseGame();
            
            if (_useExtraBorder)
                _screenBorderAnimator.ShowBorder(ScreenBorderType.ItemSelectMenuBorder);
        }
        
        protected void CloseMenu()
        {
            // For hide menu ui
            if (_useSetActive)
                gameObject.SetActive(false);
            //
            
            GameStateController.UpdateScreenState(ScreenState.InGame);
            GameStateController.ResumeGame();
            
            if (_useExtraBorder)
                _screenBorderAnimator.HideBorder();
        }
    }
}