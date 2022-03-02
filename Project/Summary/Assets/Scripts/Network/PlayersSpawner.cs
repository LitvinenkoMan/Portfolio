using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

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
        newPlayer.name += _playerScriptableObject.PlayerName;
        
        _players.Add(newPlayer);
        Debug.LogWarning("Player added");
    }
    
    public List<GameObject> GetPlayers()
    {
        return _players;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("MainMenu");
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        _players.Add(GameObject.Find(newPlayer.NickName));    
    }
}
