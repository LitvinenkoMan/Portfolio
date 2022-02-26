using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayersSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject[] SpawnPlaces;
    [SerializeField] private PlayerScriptableObject _playerScriptableObject;
    private List<GameObject> _players;
    void Start()
    {
        _players = new List<GameObject>();
        int randomSpawnPoint = Random.Range(0, SpawnPlaces.Length);
        GameObject Player = PhotonNetwork.Instantiate(_playerScriptableObject.PrefubName, SpawnPlaces[randomSpawnPoint].transform.position, SpawnPlaces[randomSpawnPoint].transform.rotation);
        _players.Add(Player);
    }
    
    

    public List<GameObject> GetPlayers()
    {
        return _players;
    }
}
