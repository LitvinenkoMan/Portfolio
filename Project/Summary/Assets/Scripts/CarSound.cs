using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour, ICarSounded
{
    [SerializeField]
    AudioSource CarSoundSource;
    [SerializeField]
    AudioClip engineOn;
    [SerializeField]
    AudioClip engineOff;
    [SerializeField]
    AudioClip carRolling;
    [SerializeField]
    AudioClip carAccceleration;


    void Start()
    {

    }

    void Update()
    {

    }

    public void CarAccelerationSound()
    {
        //CarSoundSource.clip = carAccceleration;
        //CarSoundSource.loop = true;
        //CarSoundSource.Play();
    }

    public void CarBreakSound()
    {

    }

    public void CarDriftingSound()
    {

    }

    public void CarRollingSound()
    {
        if (carRolling)
        {
            CarSoundSource.clip = carRolling;
            CarSoundSource.loop = true;
            CarSoundSource.Play();
        }
    }

    public void MuffleEngineSound()
    {
        if (engineOff)
        {
            CarSoundSource.Stop();
            CarSoundSource.PlayOneShot(engineOff);
        }
    }

    public void StartEngineSound()
    {
        if (engineOn)
        {
            CarSoundSource.PlayOneShot(engineOn);
        }
    }
}
