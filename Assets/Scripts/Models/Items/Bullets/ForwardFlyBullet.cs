using Models.Items.Bullets.Abstraction;

namespace Models.Items.Bullets
{
    public class ForwardFlyBullet : Bullet
    {
        protected override void Move()
        {
            _rb.velocity = transform.right * _firedFromWeaponStatsSo.GetSpeed();
        }
    }
}