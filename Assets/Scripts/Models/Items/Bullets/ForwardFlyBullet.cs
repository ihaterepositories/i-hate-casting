using Models.Items.Bullets.Base;

namespace Models.Items.Bullets
{
    public class ForwardFlyBullet : Bullet
    {
        protected override void Move()
        {
            _rb.velocity = transform.right * _firedFromWeaponStatsCalculator.GetSpeed();
        }
    }
}