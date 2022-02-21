using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WheelsSmokeEffect : MonoBehaviour
{
    [SerializeField] WheelCollider Wheel;
    [SerializeField] ParticleSystem smokeEffect;
    [SerializeField] private TrailRenderer trackEffect;
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
        if (collision.gameObject.layer == 3)
        {
            smokeEffect.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
            smokeEffect.Stop();
    }

    private void Update()
    {
        trackEffect.emitting = Wheel.isGrounded && trackEffect ? true : false;
        
        if (Wheel.motorTorque != 0 && Wheel.isGrounded)
        {
            smokeEffect.Emit(1);
        }
        else if (Wheel.isGrounded)
        {
            smokeEffect.Play();
            if (Wheel.brakeTorque != 0 && CarSpeed.GetCurrentSpeed() > 5)
            {
                smokeEffect.Emit(1);
            }
        }
        else smokeEffect.Stop();
    }
}