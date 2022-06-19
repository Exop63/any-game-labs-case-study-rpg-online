using System.Collections.Generic;
using Extensions;
using UnityEngine;
public class Sword : MonoBehaviour
{
    public string prafab;
    public Character Owner => m_charcter;

    [SerializeField]
    internal Character m_charcter;

    public virtual void Attacking() { }
    public virtual void Set(Character character)
    {
        m_charcter = character;
    }
}