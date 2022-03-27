using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class RaceMember : MonoBehaviour, IRacer
{
    [SerializeField] private int LapCount = 3;
    [SerializeField] private RaceCheckPointScript[] CheckPoints;
    [SerializeField] private GameObject TextContainer;
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private TextMeshProUGUI LapText;
    private bool _playerStartedRace;
    private bool _CheckPointCrossed;
    private float _timer;
    private float _afterCheckpointTimer;
    private int _currentLap = 0;

    public IEnumerator ResultHandler()
    {
        yield return new WaitForSeconds(10);
        _currentLap = 0;
        _timer = 0;
        TextContainer.SetActive(false);     
        LapText.enabled = false;
        TimerText.text = "0";
        _playerStartedRace = false;
        _afterCheckpointTimer = 5;
        yield return null;
    }

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
                _CheckPointCrossed = false;
            }
            
            if (_currentLap == LapCount)
            {
                _playerStartedRace = false;
                LapText.text = $"Finished";
                StartCoroutine("ResultHandler");
            }
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
            _currentLap += 1;
            _CheckPointCrossed = true;
        }

        if (other.GetComponent<Race>() && !_playerStartedRace)
        {
            _playerStartedRace = true;
            TextContainer.SetActive(true);
            LapText.enabled = true;
        }
    }

    public void ShowTimerValues()
    {
        TimerText.text = $"{Mathf.Round(_timer)} sec.";
        LapText.text = $"Lap: {_currentLap + 1}";
    }
}