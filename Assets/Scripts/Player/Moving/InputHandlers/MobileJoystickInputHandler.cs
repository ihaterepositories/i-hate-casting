using Player.Moving.Interfaces;
using UnityEngine;

namespace Player.Moving.InputHandlers
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
    }
}