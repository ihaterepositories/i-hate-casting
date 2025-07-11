using Models.Bullets.Abstraction;

namespace Models.Bullets
{
    public class ForwardFlyBullet : Bullet
    {
        protected override void Move()
        {
            rb.velocity = transform.right * FiredFromWeaponStats.GetSpeed();
        }
    }
}