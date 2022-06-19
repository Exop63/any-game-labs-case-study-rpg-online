using System.Collections.Generic;
using Extensions;
using UnityEngine;
public class LongSword : Sword, IMeleeWeapon
{
    public float attckTime = 1;
    public int Damage { get; set; }


    private Collider _collider;
    private Dictionary<int, Character> hitCharacters = new Dictionary<int, Character>();

    void Start()
    {
        prafab = "long-sword";
        TryGetComponent<Collider>(out _collider);
    }

    public override void Attacking()
    {
        _collider.enabled = true;
        hitCharacters = new Dictionary<int, Character>();
        EventSystem.Instance.WaitAndDo(1, () => _collider.enabled = false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hitCharacters.ContainsKey(other.GetInstanceID()))
        {
            if (TryGetComponent<Character>(out var character))
            {
                hitCharacters.Add(other.GetInstanceID(), character);
                character.TakeDamage(Damage);
            }
        }
    }
}