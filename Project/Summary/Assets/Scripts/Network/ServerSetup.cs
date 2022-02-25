using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ServerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerScriptableObject _playerScriptableObject;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    
    public override void OnJoinedLobby()
    {
        if (_playerScriptableObject.IsRoomCreator)
        {
            PhotonNetwork.CreateRoom(_playerScriptableObject.RoomName);
        }
        else
        {
            PhotonNetwork.JoinRoom(_playerScriptableObject.RoomName);
        }
    }
    
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = _playerScriptableObject.PlayerName;
        PhotonNetwork.LoadLevel("CarArcade");
    }
}
