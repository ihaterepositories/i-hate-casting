using Core.RoundControl.Enums;
using UnityEngine;

namespace Core.RoundControl
{
    public static class GameStateController
    {
        private static ScreenState _screenState;
        private static bool _isGamePaused;
        
        public static void UpdateScreenState(ScreenState newState)
        {
            _screenState = newState;
        }
        
        public static void PauseGame()
        {
            Time.timeScale = 0;
            _isGamePaused = true;
        }
        
        public static void ResumeGame()
        {
            Time.timeScale = 1;
            _isGamePaused = false;
        }
        
        public static bool IsPlayerInGame() => _screenState == ScreenState.InGame;
        public static bool IsPlayerInSomeMenu() => _screenState == ScreenState.InSomeMenu;
        public static bool IsGamePaused() => _isGamePaused;
    }
}