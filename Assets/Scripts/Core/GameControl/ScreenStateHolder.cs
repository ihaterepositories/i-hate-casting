using Core.GameControl.Enums;

namespace Core.GameControl
{
    public static class ScreenStateHolder
    {
        private static ScreenState _screenState;

        public static void UpdateScreenState(ScreenState newState)
        {
            _screenState = newState;
        }
        
        public static bool IsPlayerInGame() => _screenState == ScreenState.InGame;
        public static bool IsPlayerInSomeMenu() => _screenState == ScreenState.InSomeMenu;
    }
}