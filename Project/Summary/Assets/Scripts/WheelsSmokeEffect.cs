using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WheelsSmokeEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem Effect;

    /*CarMovment car;

    [Inject]
    void Constract(CarMovment car) 
    {
        this.car = car;
    }*/

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == 3)
    //    {
    //        Effect.Play();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == 3)
    //        Effect.Stop();
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Effect.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
            Effect.Stop();
    }

    private void Update()
    {
        if (CheckForTakingRace())
        {
            Effect.Emit(1); 
        }
    }

    bool CheckForTakingRace() 
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.Space))
            return true;
        else return false;
    }
}
