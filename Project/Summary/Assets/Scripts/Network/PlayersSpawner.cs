using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayersSpawner : MonoBehaviour
{
    [SerializeField] public GameObject PlayerPrefab;
    [SerializeField] private GameObject[] SpawnPlaces;
    void Start()
    {
        int randomSpawnPoint = Random.Range(0, SpawnPlaces.Length);
        PhotonNetwork.Instantiate(PlayerPrefab.name, SpawnPlaces[randomSpawnPoint].transform.position, SpawnPlaces[randomSpawnPoint].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
