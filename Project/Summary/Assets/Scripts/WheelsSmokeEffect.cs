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
        if (Wheel.motorTorque != 0 && Wheel.isGrounded)
        {
            Effect.Emit(1);
        }
        else if (Wheel.isGrounded)
        {
            Effect.Play();
            if (Wheel.brakeTorque != 0 && CarSpeed.GetCurrentSpeed() > 5)
            {
                Effect.Emit(1);
            }
        }
    }
}
