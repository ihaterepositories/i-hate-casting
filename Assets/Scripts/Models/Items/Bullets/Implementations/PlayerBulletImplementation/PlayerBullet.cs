using Models.Items.Bullets.Base;

namespace Models.Items.Bullets.Implementations.PlayerBulletImplementation
{
    public class PlayerBullet : Bullet
    {
        protected override void Move()
        {
            _rb.velocity = transform.right * _firedFromWeaponStatsCalculator.GetSpeed();
        }
    }
}