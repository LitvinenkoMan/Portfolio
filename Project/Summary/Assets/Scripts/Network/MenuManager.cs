using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField]private TMP_InputField PlayerName;
    [SerializeField]private TMP_InputField CreateRoomName;
    [SerializeField]private TMP_InputField CreateRoomPassword;
    [SerializeField]private TMP_InputField ConnectRoomName;
    [SerializeField]private TMP_InputField ConnectRoomPassword;
    [SerializeField]private NetworkManager _networkManager;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void JoinRoom()
    {
        if (!NetworkManager.Singleton.IsClient)
        {
            PhotonNetwork.JoinRoom(ConnectRoomName.text);
            NetworkManager.Singleton.StartClient();
        }   
        //SceneManager.LoadScene("CarArcade");

    }

    public void CreateRoom()
    {
        if (!NetworkManager.Singleton.IsClient)
        {
            PhotonNetwork.CreateRoom(CreateRoomName.text);
            NetworkManager.Singleton.StartClient();
        }   
        //SceneManager.LoadScene("CarArcade");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Sound()
    {
        
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("CarArcade");
        NetworkManager.Singleton.StartClient();
    }
}
