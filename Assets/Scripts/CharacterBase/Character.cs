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

    #region Assignments

    public int Id => m_id;

    protected Transform playerCamaeraRoot;

    protected ThirdPersonController thirdPersonController;

    public Sword EquiptedWeapon => _equiptedWeapon;


    protected StarterAssetsInputs _input;

    protected Transform WeaponRoot;
    protected Sword _equiptedWeapon;
    private HealthBar healthBar;

    [SerializeField] private int m_id;

    #endregion

    #region Status Variables
    public int Armor { get; set; }
    public int Life { get; set; }
    public Health health = new Health();
    public CharacterActions Action => action;

    private CharacterActions action;

    #endregion

    #region Animation Variables
    protected Animator _animator;
    protected bool _hasAnimator;

    #endregion

    #region  Network Variables

    internal PhotonView photonView;
    protected bool isMine;

    #endregion


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
        action = CharacterActions.Idle;
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

    /// </summary>
    /// Check the weapon can damage to me
    /// <param name="weapon"> The weapon </param>
    /// <summary>
    public bool CanDamage(IDamagabele weapon)
    {
        return Id != weapon.Owner.Id && action != CharacterActions.Dead;
    }




    #endregion
    #region  RPCs
    /// </summary>
    /// Change health of character
    /// <param name="value"> amount of health </param>
    /// <summary>
    [PunRPC]
    public void ChangeHealth(float value)
    {
        health.CurrentHealth = value;
        // Player is die
        if (health.CurrentHealth <= 0)
        {
            photonView.RPC("Death", RpcTarget.All);
        }
    }
    /// </summary>
    /// Change the unique id of the user
    /// <param name="id"> new id value </param>
    /// <summary>
    [PunRPC]
    public void SetId(int id)
    {
        m_id = id;
    }

    [PunRPC]
    public void Death()
    {
        action = CharacterActions.Dead;
        _animator.SetTrigger("Death");
        thirdPersonController.enabled = false;
        // EventSystem.Instance.WaitAndDo(2, () =>
        // {
        //     photonView.RPC("ReSpawn", RpcTarget.All);
        // });

    }
    [PunRPC]
    public void ReSpawn()
    {
        Debug.Log("ReSpawn");

        if (isMine)
        {
            var newPos = UnityEngine.Random.onUnitSphere * 10;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }

        health = new Health();
        thirdPersonController.enabled = true;


    }
    #endregion
}

