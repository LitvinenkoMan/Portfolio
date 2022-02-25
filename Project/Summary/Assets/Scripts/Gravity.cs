using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] Rigidbody ObjectRigidbody;
    [SerializeField] bool IsGravityOn = false;
    [SerializeField] bool SleepUntilTouch = true;

    [SerializeField] Vector3 CenterOfMass;
    [SerializeField] Vector3 GravityDirection = new Vector3(0, -1, 0);
    [SerializeField, Min(0)] float force = 1f;

    private void Start()
    {
        if (!ObjectRigidbody)
        {
            ObjectRigidbody = gameObject.GetComponent<Rigidbody>();
            if (!ObjectRigidbody)
            {
                ObjectRigidbody = gameObject.GetComponentInParent<Rigidbody>();
            }

            ObjectRigidbody.centerOfMass = CenterOfMass;
        }
        else
        {
            ObjectRigidbody.centerOfMass = CenterOfMass;
        }

        if (SleepUntilTouch && ObjectRigidbody)
        {
            ObjectRigidbody.Sleep();
        }
    }

    public void ActivateGravity()
    {
        IsGravityOn = true;
    }

    public void DeactivateGravity()
    {
        IsGravityOn = false;
    }

    void CalculateGravity()
    {
        if (!ObjectRigidbody.IsSleeping())
        {
            ObjectRigidbody.AddForce(GravityDirection * force * ObjectRigidbody.mass);
        }
    }

    private void FixedUpdate()
    {
        if (SleepUntilTouch)
        {
            ObjectRigidbody.Sleep();
        }
        if (IsGravityOn)
        {
            CalculateGravity();
        }
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(ObjectRigidbody.worldCenterOfMass, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(ObjectRigidbody.worldCenterOfMass, ObjectRigidbody.worldCenterOfMass + GravityDirection);
        }
    }
#endif

    [ContextMenu("Define Rigidbody component")]
    public void DefineRigidbody()
    {
        if (gameObject.GetComponent<Rigidbody>())
        {
            ObjectRigidbody = gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SleepUntilTouch = false;
    }
}