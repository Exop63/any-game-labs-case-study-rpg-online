using System.Collections.Generic;
using Extensions;
using UnityEngine;
public class Sword : MonoBehaviour, IDamagabele
{

    [SerializeField]
    public Character m_charcter;

    public Character Owner => m_charcter;

    public virtual void Attacking() { }
    public virtual void Set(Character character)
    {
        m_charcter = character;
    }
}