using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayersSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject[] SpawnPlaces;
    [SerializeField] private PlayerScriptableObject _playerScriptableObject;
    void Start()
    {
        int randomSpawnPoint = Random.Range(0, SpawnPlaces.Length);
        PhotonNetwork.Instantiate(_playerScriptableObject.PrefubName, SpawnPlaces[randomSpawnPoint].transform.position, SpawnPlaces[randomSpawnPoint].transform.rotation);
    }
    
    

    public void GetPlayers()
    {
        
        Player[] players =  PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            //players[i].m;
        }
    }
}
