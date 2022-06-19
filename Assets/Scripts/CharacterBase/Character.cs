using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Extensions;
using Photon.Pun;
using StarterAssets;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int Armor { get; set; }
    public int Life { get; set; }


    protected Transform playerCamaeraRoot;

    internal PhotonView photonView;
    protected ThirdPersonController thirdPersonController;

    protected bool isMine;
    protected Animator _animator;
    protected bool _hasAnimator;

    protected StarterAssetsInputs _input;

    protected Transform WeaponRoot;
    public Sword weapon;


    #region MonoBeahviourCallBacks
    void Awake()
    {
        SetupPlayer();
    }


    #endregion

    #region Setups

    void SetupPlayer()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView == null)
        {
            Debug.LogError("Character don't have a PhotonView component");
            PhotonNetwork.Destroy(gameObject);
        }
        isMine = photonView.IsMine;
        // setup components
        _hasAnimator = TryGetComponent(out _animator);
        TryGetComponent<ThirdPersonController>(out thirdPersonController);
        TryGetComponent<StarterAssetsInputs>(out _input);
        WeaponRoot = Extensions.GameObjectOperations.GetChild(transform, "WeaponRoot");

        if (!isMine) return; // setup input for online character
        SetupPlayerCamara();


    }

    private void SetupPlayerCamara()
    {
        playerCamaeraRoot = Extensions.GameObjectOperations.GetChild(transform, "PlayerCameraRoot");
        var playerFollowCamera = FindObjectOfType<CinemachineVirtualCamera>();

        if (playerFollowCamera != null && playerCamaeraRoot != null)
        {
            playerFollowCamera.Follow = playerCamaeraRoot;
        }
    }
    #endregion
    #region Actions

    public void EquipWeapon(string weaponPrefab)
    {
        var prafab = Resources.Load<Sword>($"Weapons/{weaponPrefab}");
        GameObjectOperations.Clear(WeaponRoot);
        var weapon = Instantiate<Sword>(prafab, WeaponRoot, false);
        if (weapon != null)
        {
            weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public abstract void TakeDamage(int damage);

    public virtual void LockMove(float time)
    {
        thirdPersonController.lockMove = true;
        EventSystem.Instance.WaitAndDo(_animator.GetCurrentAnimatorClipInfo(0).Length, () =>
                           {
                               thirdPersonController.lockMove = false;
                           });
    }
    #endregion
}
