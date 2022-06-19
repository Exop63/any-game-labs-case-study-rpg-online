
using UnityEngine;

public interface IMeleeWeapon
{
    public int Damage { get; set; }
}

public interface IDamagabele
{
    public Character Owner { get; }


}
