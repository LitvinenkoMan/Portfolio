using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CarMovment : MonoBehaviour, IMoveable
{
    float horizontalInput;
    float verticalInput;
    float steerAngle;
    [SerializeField]
    float maxSteerAngle = 30;
    [SerializeField]
    float MotorForce = 6000;

    [SerializeField]
    WheelCollider FrontLeftWheel;
    [SerializeField]
    WheelCollider FrontRightWheel;
    [SerializeField]
    WheelCollider BackLeftWheel;
    [SerializeField]
    WheelCollider BackRightWheel;

    [SerializeField]
    Transform TFrontLeftWheel;
    [SerializeField]
    Transform TFrontRightWheel;
    [SerializeField]
    Transform TBackLeftWheel;
    [SerializeField]
    Transform TBackRightWheel;

    [SerializeField]
    Rigidbody carRigidbody;

    [SerializeField]
    bool IsRearWheelDrive = false;
    bool canMove = false;

    public void MoveBack()
    {
        if (verticalInput < 0)
        {
            if (!IsRearWheelDrive)
            {
                FrontLeftWheel.motorTorque = verticalInput * MotorForce * -1;
                FrontRightWheel.motorTorque = verticalInput * MotorForce * -1;
            }
            else
            {
                BackLeftWheel.motorTorque = verticalInput * MotorForce * -1;
                BackRightWheel.motorTorque = verticalInput * MotorForce * -1;
            }
        }
    }

    public void MoveLeft()
    {
        if (steerAngle < 0)
        {
            FrontLeftWheel.steerAngle = steerAngle;
            FrontRightWheel.steerAngle = steerAngle;
        }
    }

    public void MoveRight()
    {
        if (steerAngle > 0)
        {
            FrontLeftWheel.steerAngle = steerAngle;
            FrontRightWheel.steerAngle = steerAngle;
        }
    }

    public void MoveFront()
    {

        if (verticalInput >= 0)
        {
            if (!IsRearWheelDrive)
            {
                FrontLeftWheel.motorTorque = verticalInput * MotorForce * -1;
                FrontRightWheel.motorTorque = verticalInput * MotorForce * -1;
            }
            else
            {
                BackLeftWheel.motorTorque = verticalInput * MotorForce * -1;
                BackRightWheel.motorTorque = verticalInput * MotorForce * -1;
            }
        }
    }

    public void StartMovment()
    {
        canMove = true;
    }

    public void StopMovment()
    {

    }

    void HandBreak()
    {
        FrontLeftWheel.brakeTorque = 0;
        FrontRightWheel.brakeTorque = 0;
        BackLeftWheel.brakeTorque = 0;
        BackRightWheel.brakeTorque = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            if (!IsRearWheelDrive)
            {
                BackLeftWheel.brakeTorque = MotorForce;
                BackRightWheel.brakeTorque = MotorForce;
            }
            else
            {
                FrontLeftWheel.brakeTorque = MotorForce;
                FrontRightWheel.brakeTorque = MotorForce;
            }
        }
    }

    void UpdateWheeelsModels()
    {
        UpdateWheelModel(FrontLeftWheel, TFrontLeftWheel);
        UpdateWheelModel(FrontRightWheel, TFrontRightWheel);
        UpdateWheelModel(BackLeftWheel, TBackLeftWheel);
        UpdateWheelModel(BackRightWheel, TBackRightWheel);
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
        WheelCollider[] wheelColliders = gameObject.GetComponentsInChildren<WheelCollider>();
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            if (wheelColliders[i].transform.position.y > gameObject.transform.position.y)
                isWheelsUp = true;
        }

        if (isWheelsUp && Input.GetKeyDown(KeyCode.R))
        {
            carRigidbody.AddForce(Vector3.up * carRigidbody.mass * 100 * Time.deltaTime);
            gameObject.transform.DORotate(new Vector3(180, 0, 180), 1f);
        }
    }

    void Start()
    {
        StartMovment();
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        steerAngle = maxSteerAngle * horizontalInput;
        if (canMove)
        {
            UpdateWheeelsModels();
            MoveBack();
            MoveFront();
            MoveLeft();
            MoveRight();
            ReRotate();
            HandBreak();
        }
    }
}
