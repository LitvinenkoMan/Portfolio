using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkCarMovment : CarMovement
{
    [SerializeField] private PhotonView _photonView;

    void Start()
    {
        if (!_photonView)
        {
            _photonView = GetComponent<PhotonView>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_photonView.IsMine)
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
}