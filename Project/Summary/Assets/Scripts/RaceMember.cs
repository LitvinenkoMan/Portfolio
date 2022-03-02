using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceMember : MonoBehaviour, IRacer
{
    [SerializeField] private RaceCheckPointScript[] CheckPoints;
    [SerializeField] private GameObject TextContainer;    
    [SerializeField] private TextMeshProUGUI TimerText;
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
            ShowTimerValues();
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
            TextContainer.SetActive(true);
        }
    }

    public void ShowTimerValues()
    {
        TimerText.text = $"{Mathf.Round(_timer)} sec.";
    }
}
