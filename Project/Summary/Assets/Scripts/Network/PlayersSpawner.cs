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
        var newPlayer = PhotonNetwork.Instantiate(_playerScriptableObject.PrefubName, SpawnPlaces[randomSpawnPoint].transform.position,
            SpawnPlaces[randomSpawnPoint].transform.rotation);
        
        _players.Add(newPlayer);
        Debug.LogWarning("Player added");
    }

    public List<GameObject> GetPlayers()
    {
        return _players;
    }
}
