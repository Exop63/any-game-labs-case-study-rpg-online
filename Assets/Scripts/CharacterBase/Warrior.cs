using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
    public override void TakeDamage(int damage)
    {
        Life -= Mathf.Max(0, damage - Armor);
    }
}
