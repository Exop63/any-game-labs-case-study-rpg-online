using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using StarterAssets;
using UnityEngine;

public class Warrior : Character, ICanAttack
{
    void Start()
    {
        EquipWeapon("long-sword");
    }

    void Update()
    {
        Attack();

    }

    public void Attack()
    {
        _input.attack = thirdPersonController.lockMove ? false : _input.attack;
        if (_input.attack)
        {
            _input.attack = false;
            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetTrigger(thirdPersonController._animIDAttack);
                var time = _animator.GetCurrentAnimatorClipInfo(0).Length;
                LockMove(time);
            }

        }
    }

    [PunRPC]
    public override void TakeDamage(int damage)
    {
        Life -= Mathf.Max(0, damage - Armor);
    }

}
