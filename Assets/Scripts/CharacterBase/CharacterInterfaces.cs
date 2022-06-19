using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interfaces

public interface ICanAttack
{
    void Attack();
}

public interface ICanCharge
{
    void Charge();//Warriors' special attack
}

public interface ICanCastSpell
{
    void CastSpell();//Mages' special attack
}

public interface ICanDodge
{
    void Dodge();//Rogues' special attack
}
