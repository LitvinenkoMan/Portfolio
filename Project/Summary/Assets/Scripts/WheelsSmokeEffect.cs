using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WheelsSmokeEffect : MonoBehaviour
{
    WheelCollider Wheel;
    [SerializeField]
    ParticleSystem Effect;
    CarSpeed CarSpeed;

    private void Start()
    {
        if (GetComponentInParent<CarSpeed>())
        {
            CarSpeed = GetComponentInParent<CarSpeed>();
        }
        if (GetComponent<WheelCollider>())
        {
            Wheel = GetComponent<WheelCollider>();
        }
        else
        {
            Debug.LogWarning($"Can't find component WheelCollider to attached GameObject: {gameObject.name}");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("!!!!!");
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
        if (!Wheel.isGrounded)
        {
            Effect.Stop();
        }

        if (Wheel.isGrounded)
        {
            Effect.Play();
            if (Wheel.brakeTorque != 0 && CarSpeed.GetCurrentSpeed() > 5)
            {
                Effect.Emit(1);
            }
        }

        if (Wheel.motorTorque != 0)
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
