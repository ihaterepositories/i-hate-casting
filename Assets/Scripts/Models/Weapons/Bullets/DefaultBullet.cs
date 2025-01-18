namespace Models.Weapons.Bullets
{
    public class DefaultBullet : Bullet
    {
        protected override void Move()
        {
            rb.velocity = transform.right * Speed;
        }
    }
}