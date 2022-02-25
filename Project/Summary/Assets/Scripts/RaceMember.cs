using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMember : MonoBehaviour, IRacer
{
    [SerializeField] private RaceCheckPointScript[] CheckPoints; 
    
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GetCurrentCheckpoint()
    {
        throw new System.NotImplementedException();
    }

    public int GetLeaderboardPosition()
    {
        throw new System.NotImplementedException();
    }

    public void StartTimer()
    {
        
    }
}
