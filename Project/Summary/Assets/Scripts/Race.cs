using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Race : MonoBehaviour, IRace
{
    [SerializeField] private PlayersSpawner _playersSpawner;
    [SerializeField] private GameObject[] PositionsToStart;
    [SerializeField] private IRacer[] racers;
    private bool RaceStarted = false;
    private UnityEvent OnTimerOut;
    private UnityEvent OnRaceStarted;

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

    public void SetPlayersOnStartPosition()
    {
        var players = _playersSpawner.GetPlayers();
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = PositionsToStart[i].transform.position;
            players[i].transform.rotation = PositionsToStart[i].transform.rotation;
        }
    }

    public void CountDown()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            OnTimerOut?.Invoke();
        }
    }
}