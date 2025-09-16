using Core.Input.Interfaces;
using Models.Creatures.Items.Implementations.Weapons.Base;
using Models.Creatures.Items.Implementations.Weapons.Bullets.Base;
using Models.Creatures.Items.Implementations.Weapons.Bullets.Implementations.PlayerBulletImplementation.Pools;
using UnityEngine;
using Zenject;

namespace Models.Creatures.Items.Implementations.Weapons.Implementations.PlayerWeaponImplementation
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