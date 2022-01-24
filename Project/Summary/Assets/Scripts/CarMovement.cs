using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

[System.Serializable]
public class Wheel
{
    public WheelCollider wheelCollider;
    public Transform wheelTransform;
    public bool isSteerWheel;
    public bool isFront;
    public bool isCanAccelerate;
}

public class CarMovement : MonoBehaviour, IMoveable
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
    float DegreessPerSecond = 10;
    [SerializeField]
    float MotorForce = 6000;
    [SerializeField]
    float Breakforce = 1;

    [SerializeField]
    List<Wheel> Wheels = new List<Wheel>();

    [SerializeField]
    bool IsNegetivZ = true;

    bool canMove;
    float timer;

    public void MoveBack()
    {
        if (verticalInput < 0)
        {
            foreach (var wheel in Wheels)
            {
                if (wheel.isCanAccelerate)
                {
                    if (IsNegetivZ)
                    {
                        if (CarSpeed)
                        {
                            wheel.wheelCollider.motorTorque = verticalInput * CarSpeed.CalculateMotorTorque(MotorForce) * -1;
                        }
                        else
                            wheel.wheelCollider.motorTorque = verticalInput * MotorForce * -1;
                    }
                    if (!IsNegetivZ)
                    {
                        if (CarSpeed)
                        {
                            wheel.wheelCollider.motorTorque = verticalInput * CarSpeed.CalculateMotorTorque(MotorForce);
                        }
                        else
                            wheel.wheelCollider.motorTorque = verticalInput * MotorForce;
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
                if (wheel.isFront && wheel.isSteerWheel)
                {
                    wheel.wheelCollider.steerAngle = steerAngle;
                }
                else if (!wheel.isFront && wheel.isSteerWheel)
                {
                    wheel.wheelCollider.steerAngle = -steerAngle;
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
                if (wheel.isFront && wheel.isSteerWheel)
                {
                    wheel.wheelCollider.steerAngle = steerAngle;
                }
                else if (!wheel.isFront && wheel.isSteerWheel)
                {
                    wheel.wheelCollider.steerAngle = -steerAngle;
                }
            }
        }
    }

    public void MoveFront()
    {
        if (verticalInput > 0)
        {
            foreach (var wheel in Wheels)
            {
                if (wheel.isCanAccelerate)
                {
                    if (IsNegetivZ)
                    {
                        if (CarSpeed)
                        {
                            wheel.wheelCollider.motorTorque = verticalInput * CarSpeed.CalculateMotorTorque(MotorForce) * -1;
                        }
                        else
                            wheel.wheelCollider.motorTorque = verticalInput * MotorForce * -1;
                    }
                    if (!IsNegetivZ)
                    {
                        if (CarSpeed)
                        {
                            wheel.wheelCollider.motorTorque = verticalInput * CarSpeed.CalculateMotorTorque(MotorForce);
                        }
                        else
                            wheel.wheelCollider.motorTorque = verticalInput * MotorForce;
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
            wheel.wheelCollider.brakeTorque = 0;
        }

        if (Input.GetKey(KeyCode.Space))
            foreach (var wheel in Wheels)
            {
                wheel.wheelCollider.brakeTorque = Breakforce;
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
            UpdateWheelModel(wheel.wheelCollider, wheel.wheelTransform);
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
            if (wheel.wheelTransform.position.y > gameObject.transform.position.y)
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
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //steerAngle = MaxSteerAngle * horizontalInput;

        if (horizontalInput != 0)
        {
            if (steerAngle + MaxSteerAngle * horizontalInput / DegreessPerSecond < MaxSteerAngle && steerAngle + MaxSteerAngle * horizontalInput / DegreessPerSecond > -MaxSteerAngle)
            {
                steerAngle += MaxSteerAngle * horizontalInput / DegreessPerSecond;
            }
        }


        if (verticalInput == 0)
            foreach (var wheel in Wheels)
                wheel.wheelCollider.motorTorque = 0;

        if (horizontalInput == 0)
        {
            if (steerAngle > 0)
                steerAngle -= MaxSteerAngle / DegreessPerSecond;
            if (steerAngle < 0)
                steerAngle += MaxSteerAngle / DegreessPerSecond;
            if (steerAngle < 1 && steerAngle > -1)
            {
                steerAngle = 0;
                foreach (var item in Wheels)
                {
                    if (item.isSteerWheel)
                    {
                        item.wheelCollider.steerAngle = steerAngle;
                    }
                }
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

        if (canMove)
        {
            MoveBack();
            MoveFront();
        }

        MoveLeft();
        MoveRight();
        HandBreak();
        UpdateWheeelsModels();

        ReRotate();
    }
}