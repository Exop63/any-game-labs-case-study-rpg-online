using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
public class LongSword : Sword, IMeleeWeapon
{
    // Damage value
    [SerializeField] private int m_Damage = 20;

    // time for openning collider.
    public float attckTime = 1;
    public float delay = 0.4f;

    // Damage value
    public int Damage
    {
        get
        {
            return m_Damage;
        }
        set
        {
            m_Damage = value;
        }
    }

    // collider of the sword
    private Collider m_Collider;

    // players of get damage
    private Dictionary<int, Character> hitCharacters = new Dictionary<int, Character>();

    // Owner of this sword

    void Start()
    {
        prafab = "long-sword";
        TryGetComponent<Collider>(out m_Collider);
    }

    /// 
    public override void Set(Character character)
    {
        base.Set(character);
    }
    public override void Attacking()
    {
        hitCharacters = new Dictionary<int, Character>();
        EventSystem.Instance.WaitAndDo(delay, StartAttack);
        EventSystem.Instance.WaitAndDo(attckTime, EndAttack);
    }
    private void StartAttack()
    {
        m_Collider.enabled = true;
    }

    private void EndAttack()
    {
        m_Collider.enabled = false;
        hitCharacters = new Dictionary<int, Character>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hitCharacters.ContainsKey(other.GetInstanceID()))
        {
            if (other.TryGetComponent<Character>(out var character) && character.CanDamage(this))
            {
                Debug.Log($"Attacking {prafab} hit to {other.name} {character.Id}");

                hitCharacters.Add(other.GetInstanceID(), character);
                character.TakeDamage(Damage);
            }
        }
    }
}