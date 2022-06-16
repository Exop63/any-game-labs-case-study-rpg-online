using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using StarterAssets;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int Armor { get; set; }
    public int Life { get; set; }


    protected Transform playerCamaeraRoot;

    internal PhotonView photonView;
    protected bool isMine;
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
        if (!isMine) return;

        playerCamaeraRoot = Extensions.GameObjectOperation.GetChild(transform, "PlayerCameraRoot");
        var playerFollowCamera = FindObjectOfType<CinemachineVirtualCamera>();

        if (playerFollowCamera != null && playerCamaeraRoot != null)
        {
            playerFollowCamera.Follow = playerCamaeraRoot;
        }

    }
    #endregion
    #region Actions

    public abstract void TakeDamage(int damage);
    #endregion
}
