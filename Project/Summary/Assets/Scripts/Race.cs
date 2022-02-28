using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class Race : MonoBehaviourPun, IRace
{
    [SerializeField] private PlayersSpawner _playersSpawner;
    [SerializeField] private GameObject[] PositionsToStart;
    private IRacer[] racers;
    private bool RaceStarted = false;
    public UnityEvent OnTimerOut;
    public UnityEvent OnRaceStarted;
    private string sender;

    private float _timer;

    void Start()
    {
    }


    void Update()
    {
        if (RaceStarted)
        {
            CountDown();
        }
    }

    [PunRPC]
    public void StartRace()
    {
        if (!RaceStarted)
        {
            OnRaceStarted?.Invoke();
            SetPlayersOnStartPosition();
            RaceStarted = true;
        }
    }

    public void EndRace()
    {
        throw new System.NotImplementedException();
    }

    [PunRPC]   
    public void SetPlayersOnStartPosition()
    {
        var players = _playersSpawner.GetPlayers();
        Debug.LogWarning($"{players.Count}");
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetPhotonView().RPC("SetPositionAndRotation", RpcTarget.All, PositionsToStart[i].transform.position, PositionsToStart[i].transform.rotation.eulerAngles);
            //players[i].transform.rotation = PositionsToStart[i].transform.rotation;
        }
    }

    public void CountDown()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            OnTimerOut?.Invoke();
        }
    }
}