using Core.Input.Interfaces;
using UnityEngine;

namespace Core.Input.InputHandlers
{
    public class KeyboardInputHandler : IInputHandler
    {
        public float GetHorizontalAxisValue()
        {
            return UnityEngine.Input.GetAxisRaw("Horizontal");
        }

        public float GetVerticalAxisValue()
        {
            return UnityEngine.Input.GetAxisRaw("Vertical");
        }

        public Vector3 GetPointerPosition()
        {
            if (Camera.main is not null)
            {
                return Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            }

            Debug.LogWarning("Input handler: camera is null");
            return Vector3.zero;
        }

        public bool IsFireButtonPressed()
        {
            return UnityEngine.Input.GetMouseButton(0);
        }

        public bool IsReloadButtonPressed()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.R);
        }
        
        public bool IsBurstButtonPressed()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.LeftShift);
        }
        
        public bool IsInteractingButtonPressed()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.E);
        }

        public string GetInteractingButtonName()
        {
            return "E";
        }
    }
}