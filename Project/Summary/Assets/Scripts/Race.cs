using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race : MonoBehaviour, IRace
{
    [SerializeField] private PlayersSpawner _playersSpawner;
    [SerializeField] private GameObject[] PositionsToStart;
    [SerializeField] private IRacer[] racers;
    private bool RaceStarted = false;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void StartRace()
    {
        if (!RaceStarted)
        {
            _playersSpawner
        }
    }

    public void EndRace()
    {
        throw new System.NotImplementedException();
    }

    public void SetPlayersOnStartPosition()
    {
        
    }
}