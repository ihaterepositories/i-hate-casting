using Models.Items.Weapons.Bullets.Base;

namespace Models.Items.Weapons.Bullets.Implementations.PlayerBulletImplementation
{
    public class PlayerBullet : Bullet
    {
        protected override void Move()
        {
            _rb.velocity = transform.right * _firedFromWeaponStatsCalculator.GetSpeed();
        }
    }
}