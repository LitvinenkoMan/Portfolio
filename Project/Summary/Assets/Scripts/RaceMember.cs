using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceMember : MonoBehaviour, IRacer
{
    [SerializeField] private float raundsLeft = 1;
    [SerializeField] private RaceCheckPointScript[] CheckPoints;
    [SerializeField] private GameObject TextContainer;
    [SerializeField] private TextMeshProUGUI TimerText;
    private bool _playerStartedRace;
    private bool _CheckPointCrossed;
    private float _timer;
    private float _afterCheckpointTimer;

    void Start()
    {
        _playerStartedRace = false;
        _timer = 0;
        _afterCheckpointTimer = 5;
    }

    void Update()
    {
        if (_playerStartedRace)
        {
            RaceTimer();
            ShowTimerValues();
        }

        if (_CheckPointCrossed)
        {
            _afterCheckpointTimer -= Time.deltaTime;
            if (_afterCheckpointTimer < 0)
            {
                _afterCheckpointTimer = 5;
                if (raundsLeft < 0)
                {
                    TextContainer.SetActive(false);
                    _timer = 0;
                    raundsLeft = 1;
                }

                _CheckPointCrossed = false;
            }
        }

        if (raundsLeft < 0)
        {
            _playerStartedRace = false;
            _afterCheckpointTimer = 5;
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
        if (other.GetComponent<Race>() && _playerStartedRace && !_CheckPointCrossed)
        {
            raundsLeft -= 1;
            _CheckPointCrossed = true;
        }

        if (other.GetComponent<Race>() && !_playerStartedRace)
        {
            _playerStartedRace = true;
            TextContainer.SetActive(true);
        }
    }

    public void ShowTimerValues()
    {
        TimerText.text = $"{Mathf.Round(_timer)} sec.";
    }
}