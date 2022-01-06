using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    Rigidbody ObjectRigidbody;
    [SerializeField]
    bool IsGravityOn = false;

    [SerializeField]
    Vector3 CenterOfMass;
    [SerializeField]
    Vector3 GravityDirection = new Vector3(0, -1, 0);
    [SerializeField, Min(0)]
    float force = 1f;

    private void Start()
    {
        if (!ObjectRigidbody)
        {
            ObjectRigidbody = gameObject.GetComponent<Rigidbody>();
            ObjectRigidbody.centerOfMass = CenterOfMass;
        }
        else
        {
            ObjectRigidbody.centerOfMass = CenterOfMass;
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
        ObjectRigidbody.AddForce(GravityDirection * force * ObjectRigidbody.mass);
    }

    private void FixedUpdate()
    {
        if (IsGravityOn)
        {
            CalculateGravity();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ObjectRigidbody.worldCenterOfMass, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + GravityDirection);
    }

}
