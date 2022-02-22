using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayersSpawner : MonoBehaviour
{
    [SerializeField] public GameObject PlayerPrefab;
    [SerializeField] private GameObject[] SpawnPlaces;
    [SerializeField] private PlayerScriptableObject _playerScriptableObject;
    void Start()
    {
        int randomSpawnPoint = Random.Range(0, SpawnPlaces.Length);
        PhotonNetwork.Instantiate(_playerScriptableObject.PrefubName, SpawnPlaces[randomSpawnPoint].transform.position, SpawnPlaces[randomSpawnPoint].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
