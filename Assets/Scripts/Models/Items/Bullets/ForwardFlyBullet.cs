using Models.Items.Bullets.Abstraction;

namespace Models.Items.Bullets
{
    public class ForwardFlyBullet : Bullet
    {
        protected override void Move()
        {
            rb.velocity = transform.right * FiredFromWeaponStatsSo.GetSpeed();
        }
    }
}