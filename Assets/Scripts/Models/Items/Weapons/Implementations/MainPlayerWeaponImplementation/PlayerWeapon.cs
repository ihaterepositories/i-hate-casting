using Core.Input.Interfaces;
using Models.Items.Weapons.Base;
using Models.Items.Weapons.Bullets.Base;
using Models.Items.Weapons.Bullets.Implementations.PlayerBulletImplementation.Pools;
using UnityEngine;
using Zenject;

namespace Models.Items.Weapons.Implementations.MainPlayerWeaponImplementation
{
    public class PlayerWeapon : Weapon
    {
        private IInputHandler _inputHandler;
        private PlayerBulletsPool _bulletsPool;
        
        [Inject]
        private void Construct(IInputHandler inputHandler, PlayerBulletsPool bulletsPool)
        {
            _inputHandler = inputHandler;
            _bulletsPool = bulletsPool;
        }

        protected override bool GetFirePermission()
        {
            return _inputHandler.IsFireButtonPressed();
        }

        protected override Bullet GetBulletFromPool()
        {
            return _bulletsPool.GetFreeObject();
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