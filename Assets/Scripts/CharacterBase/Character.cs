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
    public int Id => m_id;
    public int Armor { get; set; }
    public int Life { get; set; }
    public Sword EquiptedWeapon => _equiptedWeapon;
    public Health health = new Health();


    protected Transform playerCamaeraRoot;

    internal PhotonView photonView;
    protected ThirdPersonController thirdPersonController;

    protected bool isMine;
    protected Animator _animator;
    protected bool _hasAnimator;

    protected StarterAssetsInputs _input;

    protected Transform WeaponRoot;
    protected Sword _equiptedWeapon;
    private HealthBar healthBar;

    [SerializeField] private int m_id;



    #region MonoBeahviourCallBacks
    void Awake()
    {
        SetupPlayer();
    }

    void Update()
    {
        Attack();
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

        SetupAsiggments();
        SetupStatus();
        SetupPublicUI();


        if (!isMine) return; // setup input for online character
        SetupPlayerCamara();


    }

    private void SetupStatus()
    {
        health = new Health();
    }

    private void SetupAsiggments()
    {

        isMine = photonView.IsMine;
        // setup components
        _hasAnimator = TryGetComponent(out _animator);
        TryGetComponent<ThirdPersonController>(out thirdPersonController);
        TryGetComponent<StarterAssetsInputs>(out _input);
        // setup weapon root
        WeaponRoot = Extensions.GameObjectOperations.GetChild(transform, "WeaponRoot");

        SetId();

    }

    /// <summary>
    /// Setup the user Id for damage or other things
    /// </summary>
    private void SetId()
    {
        if (isMine)
        {
            m_id = GetInstanceID();
            photonView.RPC("SetId", RpcTarget.OthersBuffered, m_id);
        }
    }
    /// </summary>
    /// Setup the UI of all user must see
    /// <summary>
    private void SetupPublicUI()
    {
        healthBar = HUD.Instance.AddPlayerHud(this);
    }

    /// </summary>
    /// Setup the Camaer of player 
    /// <summary>
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


    /// </summary>
    /// Change sword
    /// <param name="weaponPrefab"> id of sword. prefab must be located Resources/Weapons/ and the name must be weaponPrefab </param>
    /// <summary>
    public abstract void EquipWeapon(string weaponPrefab);

    /// </summary>
    /// Take Damage the other players
    /// <param name="damage"> Amount of damage taken </param>
    /// <summary>
    public abstract void TakeDamage(int damage);
    /// </summary>
    /// Attack state for player
    /// <summary>
    public abstract void Attack();

    /// </summary>
    /// Lock player movment for time
    /// <param name="time"> Time of locking </param>
    /// <summary>
    public virtual void LockMove(float time)
    {
        thirdPersonController.lockMove = true;
        EventSystem.Instance.WaitAndDo(time, () =>
                           {
                               thirdPersonController.lockMove = false;
                           });
    }
    public bool CanDamage(Sword weapon)
    {
        Debug.Log($"CanDamage Id: {Id} wepon character id: {weapon.Owner.Id}");
        return Id != weapon.Owner.Id;
    }
    [PunRPC]
    internal void ChangeHealth(float value)
    {
        health.CurrentHealth = value;
    }

    [PunRPC]
    public void SetId(int id)
    {
        m_id = id;
    }



    #endregion
}

