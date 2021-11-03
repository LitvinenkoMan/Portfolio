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
    Vector3 GravityDirection = new Vector3(0, -1, 0);
    [SerializeField, Min(0)]
    float force = 1f;

    public void ActivateGravity()
    {
        IsGravityOn = false;
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

}
