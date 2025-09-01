using Core.Input.Interfaces;
using Models.Items.Weapons.Base;
using UnityEngine;
using Zenject;

namespace Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation
{
    public class PlayerWeapon : Weapon
    {
        private IInputHandler _inputHandler;
        
        [Inject]
        private void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        protected override bool GetFirePermission()
        {
            return _inputHandler.IsFireButtonPressed();
        }

        protected override bool GetReloadPermission()
        {
            return _inputHandler.IsReloadButtonPressed();
        }

        protected override float GetFireDirectionAngle()
        {
            Vector2 lookDirection = (_inputHandler.GetPointerPosition() - transform.position);
            return Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + CalculateSpread();
        }
    }
}