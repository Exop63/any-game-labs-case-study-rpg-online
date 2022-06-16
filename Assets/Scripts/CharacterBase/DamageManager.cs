public class DamageManager
{
    public void CalculateDamage(Character character, int damage)
    {
        character?.TakeDamage(damage);
    }
}