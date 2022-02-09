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
    [SerializeField] CarSound CarSound;
    [SerializeField] CarSpeed CarSpeed;

    [SerializeField] Rigidbody carRigidbody;
    [SerializeField] float MaxSteerAngle = 30;
    [SerializeField] float DegreessPerSecond = 10;
    [SerializeField] float MotorForce = 6000;
    [SerializeField] float Breakforce = 1;

    [SerializeField] List<Wheel> Wheels = new List<Wheel>();

    [SerializeField] bool IsNegativZ = true;

    float horizontalInput;
    float verticalInput;
    float steerAngle;
    float timer;
    int _direction = 1;
    bool canMove;

    public void MoveBack()
    {
        if (verticalInput < 0)
        {
            foreach (var wheel in Wheels)
            {
                if (wheel.isCanAccelerate)
                {
                    if (CarSpeed)
                    {
                        wheel.wheelCollider.motorTorque =
                            verticalInput * CarSpeed.CalculateMotorTorque(MotorForce) * _direction;
                    }
                    else
                        wheel.wheelCollider.motorTorque = verticalInput * MotorForce * _direction;
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
                    if (CarSpeed)
                    {
                        wheel.wheelCollider.motorTorque =
                            verticalInput * CarSpeed.CalculateMotorTorque(MotorForce) * _direction;
                    }
                    else
                        wheel.wheelCollider.motorTorque = verticalInput * MotorForce * _direction;
                }
            }
        }
    }

    public void StartMovement()
    {
        canMove = true;
    }

    public void StopMovement()
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

    void UpdateWheelsModels()
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

        wheel.position = position/* + new Vector3(0, 0.1f, 0)*/;
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
        StartMovement();
        _direction = IsNegativZ ? -1 : 1;
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //steerAngle = MaxSteerAngle * horizontalInput;

        if (horizontalInput != 0)
        {
            if (steerAngle + MaxSteerAngle / DegreessPerSecond * horizontalInput < MaxSteerAngle &&
                steerAngle + MaxSteerAngle / DegreessPerSecond * horizontalInput > -MaxSteerAngle)
            {
                steerAngle += MaxSteerAngle / DegreessPerSecond * horizontalInput;
            }
        }
        else
        {
            //TODO: переделать, выглядит плохо
            if (steerAngle > 0)
                steerAngle -= MaxSteerAngle / DegreessPerSecond;
            if (steerAngle < 0)
                steerAngle += MaxSteerAngle / DegreessPerSecond;
            if (steerAngle < 3 && steerAngle > -3)
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

        MoveLeft();
        MoveRight();

        if (canMove)
        {
            MoveBack();
            MoveFront();
        }

        if (canMove && Input.GetKeyDown(KeyCode.I))
        {
            CarSound.MuffleEngine();
            StopMovement();
        }
        else if (!canMove && Input.GetKeyDown(KeyCode.I))
        {
            CarSound.StartEngine();
            CarSound.CarRolling();
            StartMovement();
        }

        HandBreak();
        if (verticalInput == 0)
            foreach (var wheel in Wheels)
                wheel.wheelCollider.motorTorque = 0;

        UpdateWheelsModels();
        ReRotate();
    }
}