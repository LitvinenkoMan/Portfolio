using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMember : MonoBehaviour, IRacer
{
    [SerializeField] private RaceCheckPointScript[] CheckPoints;
    private bool _playerStartedRace;
    private float _timer;
    
    void Start()
    {
        _playerStartedRace = false;
        _timer = 0;
    }

    void Update()
    {
        if (_playerStartedRace)
        {
            RaceTimer();
        }
    }

    public void GetCurrentCheckpoint()
    {
        throw new System.NotImplementedException();
    }

    public int GetLeaderboardPosition()
    {
        throw new System.NotImplementedException();
    }

    public void RaceTimer()
    {
        _timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Race>() && !_playerStartedRace)
        {
            _playerStartedRace = true;
        }
    }
}
