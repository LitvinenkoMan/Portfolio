using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class CameraFolow : MonoBehaviour
{
    [SerializeField]
    GameObject Target;
    Camera MainCamera;
    [Inject]
    CarManager cars;

    [SerializeField]
    bool isThirdPersonMode = false;
    [SerializeField]
    bool IsNegativZ = true;

    [SerializeField]
    Vector3 OrthographicCameraPosition = new Vector3(80, 110, 80);
    [SerializeField]
    Vector3 OrthographRotation = new Vector3(45, -135, 0);
    [SerializeField]
    float DistanceToObject = 10;

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
            if (cars)
            {
                gameObject.transform.DOMove(cars.GetActiveCar().transform.position + OrthographicCameraPosition, 0.5f);
                gameObject.transform.DORotate(OrthographRotation, 0.5f);
            }
            if (Target)
            {
                gameObject.transform.DOMove(Target.transform.position + OrthographicCameraPosition, 0.5f);
                gameObject.transform.DORotate(OrthographRotation, 0.5f);
                MainCamera.orthographicSize = 30;
            }
        }
        else ThirdPersonMode();
    }

    void ChangeCameraView()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (isThirdPersonMode)
            {
                MainCamera.orthographic = true;
                isThirdPersonMode = false;
            }
            else
            {
                MainCamera.orthographic = false;
                isThirdPersonMode = true;
            }
        }
    }

    void ThirdPersonMode()
    {
        if (cars)
        {
            MainCamera.transform.DOLookAt(cars.GetActiveCar().transform.position + new Vector3(0, 2, 0), 0.5f);
            if (IsNegativZ)
            {
                MainCamera.transform.DOMove(cars.GetActiveCar().transform.position + cars.GetActiveCar().transform.forward * DistanceToObject + new Vector3(0, 5, 0), 0.5f);
            }
            else MainCamera.transform.DOMove(cars.GetActiveCar().transform.position + cars.GetActiveCar().transform.forward * -DistanceToObject + new Vector3(0, 5, 0), 0.5f);
        }
        if (Target)
        {
            MainCamera.transform.DOLookAt(Target.transform.position + new Vector3(0, 2, 0), 0.5f);
            if (IsNegativZ)
            {
                MainCamera.transform.DOMove(Target.transform.position + Target.transform.forward * DistanceToObject + new Vector3(0, 5, 0), 0.5f);
            }
            else MainCamera.transform.DOMove(Target.transform.position + Target.transform.forward * -DistanceToObject + new Vector3(0, 5, 0), 0.5f);
        }
    }
}
