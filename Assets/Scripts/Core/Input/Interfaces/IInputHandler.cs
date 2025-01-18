using UnityEngine;

namespace Core.Input.Interfaces
{
    public interface IInputHandler
    {
        public float GetHorizontalAxisValue();
        public float GetVerticalAxisValue();
        public Vector3 GetPointerPosition();
        public bool IsShootButtonPressed();
    }
}
