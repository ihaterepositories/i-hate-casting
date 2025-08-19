using Core.GameControl.Enums;
using UnityEngine;

namespace Core.GameControl
{
    public static class GameStateHolder
    {
        private static ScreenState _screenState;
        public static bool IsGamePaused { get; set; }

        public static void UpdateScreenState(ScreenState newState)
        {
            _screenState = newState;
        }
        
        public static bool IsPlayerInGame() => _screenState == ScreenState.InGame;
        public static bool IsPlayerInSomeMenu() => _screenState == ScreenState.InSomeMenu;
    }
}