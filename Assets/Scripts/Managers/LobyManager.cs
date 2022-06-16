using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


public class LobyManager : MonoBehaviourPunCallbacks
{
    #region Assignets
    public GameObject infoText;
    public GameObject StartGameButton;
    private string gameScene = "Game";
    #endregion

    #region MonoBehaviourCallBacks
    void Awake()
    {
        Setup();
        SetupUI();
    }
    #endregion
    #region MonoBehaviourPunCallbacks
    public override void OnConnected()
    {
        base.OnConnected();


    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        infoText.SetActive(false);
        StartGameButton.SetActive(true);
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        LoadGame();
    }
    #endregion
    #region Setups
    internal void Setup()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "0.0.1";

    }
    internal void SetupUI()
    {
        infoText.SetActive(true);
        StartGameButton.SetActive(false);
    }
    #endregion
    #region  Actions

    private void StartGame()
    {
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomOrCreateRoom();

        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.

        }

    }
    internal void LoadGame()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            return;
        }
        PhotonNetwork.LoadLevel(gameScene);
    }
    #endregion

    #region Events
    // Trigger when client tab to the Start button
    public void OnClickStart()
    {
        StartGame();
    }
    #endregion

}
