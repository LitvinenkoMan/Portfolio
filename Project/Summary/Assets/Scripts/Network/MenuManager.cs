using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Zenject;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [Inject] private CarManager _carManager;
    [SerializeField] private TMP_InputField PlayerName;
    [SerializeField] private TMP_InputField CreateRoomName;
    [SerializeField] private TMP_InputField ConnectRoomName;
    [SerializeField] private PlayerScriptableObject _playerScriptableObject;


    //[SerializeField]private NetworkManager _networkManager;
    void Start()
    {
    }


    void Update()
    {
        
    }

    public void JoinRoom()
    {
        _playerScriptableObject.PrefubName = _carManager.GetActiveCar().name;
        _playerScriptableObject.PlayerName = PlayerName.text;
        PhotonNetwork.JoinRoom(ConnectRoomName.text);
    }

    public void CreateRoom()
    {
        _playerScriptableObject.PrefubName = _carManager.GetActiveCar().name;
        _playerScriptableObject.PlayerName = PlayerName.text;
        PhotonNetwork.CreateRoom(CreateRoomName.text);
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
    }
}