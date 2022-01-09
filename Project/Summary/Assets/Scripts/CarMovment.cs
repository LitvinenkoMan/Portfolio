using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

[System.Serializable]
public class Wheel
{
    public WheelCollider WheelCollider;
    public Transform WheelTransform;
    public bool IsSteerWheel;
    public bool IsFront;
    public bool IsCanAccelerate;
}

public class CarMovment : MonoBehaviour, IMoveable
{
    [SerializeField]
    CarSound CarSound;
    [SerializeField]
    CarSpeed CarSpeed;
    float horizontalInput;
    float verticalInput;
    float steerAngle;

    [SerializeField]
    Rigidbody carRigidbody;
    [SerializeField]
    float MaxSteerAngle = 30;
    [SerializeField]
    float MotorForce = 6000;
    [SerializeField]
    float Breakforce = 1;

    [SerializeField]
    List<Wheel> Wheels = new List<Wheel>();

    [SerializeField]
    bool IsNegetivZ = true;

    bool canMove = false;

    public void MoveBack()
    {
        if (verticalInput < 0)
        {
            foreach (var wheel in Wheels)
            {
                if (wheel.IsCanAccelerate)
                {
                    if (IsNegetivZ)
                    {
                        if (CarSpeed)
                        {
                            wheel.WheelCollider.motorTorque = verticalInput * CarSpeed.CalculateMotorTorque(MotorForce) * -1;
                        }
                        else
                            wheel.WheelCollider.motorTorque = verticalInput * MotorForce * -1;
                    }
                    if (!IsNegetivZ)
                    {
                        if (CarSpeed)
                        {
                            wheel.WheelCollider.motorTorque = verticalInput * CarSpeed.CalculateMotorTorque(MotorForce);
                        }
                        else
                            wheel.WheelCollider.motorTorque = verticalInput * MotorForce;
                    }
                }
            }
        }
    }

    public void MoveLeft()
    {
        if (steerAngle < 0)
        {
            foreach (var wheel in Wheels)
            {
                if (wheel.IsFront && wheel.IsSteerWheel)
                {
                    wheel.WheelCollider.steerAngle = steerAngle;
                }
                else if (!wheel.IsFront && wheel.IsSteerWheel)
                {
                    wheel.WheelCollider.steerAngle = -steerAngle;
                }
            }
        }
    }

    public void MoveRight()
    {
        if (steerAngle > 0)
        {
            foreach (var wheel in Wheels)
            {
                if (wheel.IsFront && wheel.IsSteerWheel)
                {
                    wheel.WheelCollider.steerAngle = steerAngle;
                }
                else if (!wheel.IsFront && wheel.IsSteerWheel)
                {
                    wheel.WheelCollider.steerAngle = -steerAngle;
                }
            }
        }
    }

    public void MoveFront()
    {
        if (verticalInput > 0)
        {
            if (CarSound) { CarSound.CarAccelerationSound(); }

            foreach (var wheel in Wheels)
            {
                if (wheel.IsCanAccelerate)
                {
                    if (IsNegetivZ)
                    {
                        if (CarSpeed)
                        {
                            wheel.WheelCollider.motorTorque = verticalInput * CarSpeed.CalculateMotorTorque(MotorForce) * -1;
                            //Debug.Log(wheel.WheelCollider.motorTorque);
                        }
                        else
                            wheel.WheelCollider.motorTorque = verticalInput * MotorForce * -1;
                    }
                    if (!IsNegetivZ)
                    {
                        if (CarSpeed)
                        {
                            wheel.WheelCollider.motorTorque = verticalInput * CarSpeed.CalculateMotorTorque(MotorForce);
                            //Debug.Log(wheel.WheelCollider.motorTorque);
                        }
                        else
                            wheel.WheelCollider.motorTorque = verticalInput * MotorForce;
                    }
                }
            }
        }
    }

    public void StartMovment()
    {
        canMove = true;
    }

    public void StopMovment()
    {
        canMove = false;
    }

    void HandBreak()
    {
        foreach (var wheel in Wheels)
        {
            wheel.WheelCollider.brakeTorque = 0;
        }

        if (Input.GetKey(KeyCode.Space))
            foreach (var wheel in Wheels)
            {
                wheel.WheelCollider.brakeTorque = Breakforce;
            }
    }

    public List<Wheel> GetCarWheels()
    {
        return Wheels;
    }

    void UpdateWheeelsModels()
    {
        foreach (var wheel in Wheels)
        {
            UpdateWheelModel(wheel.WheelCollider, wheel.WheelTransform);
        }
    }

    void UpdateWheelModel(WheelCollider wheelCollider, Transform wheel)
    {
        Vector3 position = wheel.position;
        Quaternion quaternion = wheel.rotation;

        wheelCollider.GetWorldPose(out position, out quaternion);

        wheel.position = position + new Vector3(0, 0.1f, 0);
        wheel.rotation = quaternion;
    }

    void ReRotate()
    {
        bool isWheelsUp = false;

        foreach (var wheel in Wheels)
        {
            if (wheel.WheelTransform.position.y > gameObject.transform.position.y)
            {
                isWheelsUp = true;
                break;
            }
        }
        if (isWheelsUp && Input.GetKeyDown(KeyCode.R))
        {
            carRigidbody.AddForce(Vector3.up * carRigidbody.mass * 1000);
            carRigidbody.AddTorque(Vector3.right * 180 * carRigidbody.mass * 100);
            //gameObject.transform.DORotate(new Vector3(180, 0, 180), 1f);
        }
    }

    public bool IsCarCanMove()
    {
        return canMove;
    }

    void Start()
    {
        StartMovment();
    }

    private void FixedUpdate()
    {
        //  Debug.Log($"locla rotation {}");
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        steerAngle = MaxSteerAngle * horizontalInput;

        if (verticalInput == 0)
            foreach (var wheel in Wheels)
            {
                wheel.WheelCollider.motorTorque = 0;
            }

       

        if (horizontalInput == 0)
            foreach (var wheel in Wheels)
            {
                if (wheel.IsSteerWheel)
                {
                    wheel.WheelCollider.steerAngle = 0;
                }
            }

        
        if (canMove && Input.GetKeyDown(KeyCode.I))
        {
            CarSound.MuffleEngineSound();
            StopMovment();
        }
        else if (!canMove && Input.GetKeyDown(KeyCode.I))
        {
            CarSound.StartEngineSound();
            CarSound.CarRollingSound();
            StartMovment();
        }

        UpdateWheeelsModels();
        MoveLeft();
        MoveRight();
        HandBreak();
        ReRotate();

        if (canMove)
        {
            MoveBack();
            MoveFront();
        }

    }
}