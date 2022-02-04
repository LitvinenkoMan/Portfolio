using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour, ICarSounded
{
    [SerializeField]
    AudioSource CarSoundSource;
    [SerializeField]
    AudioClip engineOnSound;
    [SerializeField]
    AudioClip engineOffSound;
    [SerializeField]
    AudioClip carRollingSound;
    [SerializeField]
    AudioClip carAcccelerationSound;

    private void Start()
    {
        if (!CarSoundSource || !engineOnSound || !engineOffSound || !carRollingSound || !carAcccelerationSound)
        {
            Debug.LogWarning("There is one or more sounds ");
        }
    }

    public void CarAcceleration()
    {
        //CarSoundSource.clip = carAccceleration;
        //CarSoundSource.loop = true;
        //CarSoundSource.Play();
    }

    public void CarBreak()
    {

    }

    public void CarDrifting()
    {

    }

    public void CarRolling()
    {
        if (carRollingSound)
        {
            CarSoundSource.clip = carRollingSound;
            CarSoundSource.loop = true;
            CarSoundSource.Play();
        }
    }

    public void MuffleEngine()
    {
        if (engineOffSound)
        {
            CarSoundSource.Stop();
            CarSoundSource.PlayOneShot(engineOffSound);
        }
    }

    public void StartEngine()
    {
        if (engineOnSound)
        {
            CarSoundSource.PlayOneShot(engineOnSound);
        }
    }
}
