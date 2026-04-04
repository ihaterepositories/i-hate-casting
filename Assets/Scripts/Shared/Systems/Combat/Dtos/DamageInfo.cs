namespace Shared.Systems.Combat.Dtos
{
    public class DamageInfo
    {
        public DamageInfo(float damageToDeal)
        {
            DamageToDeal = damageToDeal;
        }
        
        public float DamageToDeal { get; }
    }
}