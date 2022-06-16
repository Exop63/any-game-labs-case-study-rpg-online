using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MatchManager : MonoBehaviourPunCallbacks
{
    public string playerPrefabName;

    void Start()
    {
        CreatePlayer();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("OnJoinedRoom");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("OnPlayerEnteredRoom");
    }
    public void CreatePlayer()
    {
        PhotonNetwork.Instantiate(playerPrefabName, Vector3.zero, Quaternion.identity);
    }

}
