using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsSmokeEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem Effect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Effect.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
            Effect.Stop();
    }

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
}
