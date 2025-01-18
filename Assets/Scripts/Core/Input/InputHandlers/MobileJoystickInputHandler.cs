using Core.Input.Interfaces;
using UnityEngine;

namespace Core.Input.InputHandlers
{
    public class MobileJoystickInputHandler : MonoBehaviour, IInputHandler
    {
        [SerializeField] private FixedJoystick mobileJoystick;
        
        public float GetHorizontalAxisValue()
        {
            return mobileJoystick.Horizontal;
        }

        public float GetVerticalAxisValue()
        {
            return mobileJoystick.Vertical;
        }

        public Vector3 GetPointerPosition()
        {
            throw new System.NotImplementedException();
        }

        public bool IsShootButtonPressed()
        {
            throw new System.NotImplementedException();
        }
    }
}