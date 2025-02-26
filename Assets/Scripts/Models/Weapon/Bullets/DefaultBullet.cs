namespace Models.Weapon.Bullets
{
    public class DefaultBullet : Bullet
    {
        protected override void Move()
        {
            rb.velocity = transform.right * stats.speed;
        }
    }
}