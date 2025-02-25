using Core.Input.Interfaces;
using UnityEngine;
using Zenject;

namespace Models.Weapon.Realizations
{
    public class PlayerWeapon : Abstraction.Weapon
    {
        private IInputHandler _inputHandler;
        
        [Inject]
        private void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        protected override bool GetFirePermission()
        {
            return _inputHandler.IsShootButtonPressed();
        }

        protected override Vector3 GetTargetPosition()
        {
            return _inputHandler.GetPointerPosition();
        }
    }
}