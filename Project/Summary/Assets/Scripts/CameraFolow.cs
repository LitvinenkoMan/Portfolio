using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class CameraFolow : MonoBehaviour
{
    [SerializeField]
    Transform cameraTarget;
    [SerializeField]
    Transform playerModel;
    [SerializeField]
    float minimumAngle;
    [SerializeField]
    float maximumAngle;
    [SerializeField]
    float mouseSensitivity;

    Camera MainCamera;

    CarManager cars;

    [SerializeField]
    bool isThirdPersonMode = false;

    [SerializeField]
    Vector3 OrthographicCameraPòosition = new Vector3(80, 110, 80);
    [SerializeField]
    Vector3 NoneOrthographicCameraPosition = new Vector3(0, 10, 15);
    [SerializeField]
    Vector3 OrthographRotation = new Vector3(45, -135, 0);


    [Inject]
    void Constract(CarManager car)
    {
        this.cars = car;
    }

    private void Start()
    {
        MainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        ChangeCameraView();

        if (!isThirdPersonMode) 
        { 
            gameObject.transform.DOMove(cars.GetActiveCar().transform.position + OrthographicCameraPòosition, 0.5f);
            gameObject.transform.DORotate(OrthographRotation, 0.5f);
        }
        else ThirdPersonMode();
    }

    void ChangeCameraView()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (isThirdPersonMode)
            {
                //MainCamera.transform.SetParent(cars.GetActiveCar().transform.parent);
                MainCamera.orthographic = true;
                isThirdPersonMode = false;
            }
            else 
            {
                //MainCamera.transform.SetParent(cars.GetActiveCar().transform);
                MainCamera.orthographic = false;
                isThirdPersonMode = true; 
            }

            //if (MainCamera.orthographic)
            //{
            //    MainCamera.orthographic = false;
            //    gameObject.transform.DOMove(cars.GetActiveCar().transform.forward , 0.5f);
            //    //MainCamera.transform.DOLookAt(car.transform.position, 0.5f);
            //}
            //else
            //{
            //    //MainCamera.transform.DORotate(OrthographRotation, 0.5f);
            //    MainCamera.transform.SetParent(cars.transform.parent);
            //    gameObject.transform.DOMove(cars.transform.localPosition + OrthographicCameraPosition, 0.5f);
            //    MainCamera.orthographic = true;
            //}
        }
    }

    void ThirdPersonMode()
    {
        float aimX = Input.GetAxis("Mouse X");
        float aimY = Input.GetAxis("Mouse Y");

        MainCamera.transform.DOLookAt(cars.GetActiveCar().transform.position+ new Vector3(0, 2, 0), 0.5f);

        MainCamera.transform.DOMove(cars.GetActiveCar().transform.position + cars.GetActiveCar().transform.forward * 15 + new Vector3(0,5,0), 0.5f);

        //var angleX = cameraTarget.localEulerAngles.x;
        //if (angleX > 180 && angleX < maximumAngle)
        //{
        //    angleX = maximumAngle;
        //}
        //else if (angleX < 180 && angleX > minimumAngle)
        //{
        //    angleX = minimumAngle;
        //}

        //cameraTarget.localEulerAngles = new Vector3(angleX, cameraTarget.localEulerAngles.y, 0);

    }
}
