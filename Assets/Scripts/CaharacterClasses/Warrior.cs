using System.Collections;
using System.Collections.Generic;
using Extensions;
using Photon.Pun;
using StarterAssets;
using UnityEngine;

public class Warrior : Character, ICanAttack
{
    void Start()
    {
        EquipWeapon("long-sword");
    }


    public override void Attack()
    {
        _input.attack = thirdPersonController.lockMove ? false : _input.attack;
        if (_input.attack)
        {
            _input.attack = false;
            EquiptedWeapon.Attacking();
            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetTrigger(thirdPersonController._animIDAttack);
                var time = _animator.GetCurrentAnimatorClipInfo(0).Length;
                LockMove(time);
            }

        }
    }
    public override void EquipWeapon(string weaponPrefab)
    {
        var prafab = Resources.Load<Sword>($"Weapons/{weaponPrefab}");
        GameObjectOperations.Clear(WeaponRoot);
        _equiptedWeapon = Instantiate<Sword>(prafab, WeaponRoot, false);
        if (_equiptedWeapon != null)
        {
            _equiptedWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
        _equiptedWeapon.Set(this);
    }
    public override void TakeDamage(int damage)
    {
        var newHealth = health.CurrentHealth - Mathf.Max(0, damage - Armor);
        ChangeHealth(newHealth);
        photonView.RPC("ChangeHealth", RpcTarget.Others, newHealth);
    }

}
