using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class CameraFolow : MonoBehaviour
{
    [SerializeField] GameObject Target;
    Camera Camera;
    [Inject] CarManager cars;

    [SerializeField] bool isThirdPersonMode = false;
    [SerializeField] bool IsNegativeZ = true;

    [SerializeField] Vector3 OrthographicCameraPosition = new Vector3(80, 110, 80);
    [SerializeField] Vector3 OrthographRotation = new Vector3(45, -135, 0);
    [SerializeField] float DistanceToObject = 10;

    private void Start()
    {
        Camera = Camera.main;
        if (!Camera)
        {
            Camera = GetComponent<Camera>();
        }

        Camera.orthographicSize = 50;
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
                Camera.orthographicSize = 30;
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
                Camera.orthographic = true;
                isThirdPersonMode = false;
            }
            else
            {
                Camera.orthographic = false;
                isThirdPersonMode = true;
            }
        }
    }

    void ThirdPersonMode()
    {
        if (cars)
        {
            Camera.transform.DOLookAt(cars.GetActiveCar().transform.position + new Vector3(0, 2, 0), 0.5f);
            if (IsNegativeZ)
            {
                Camera.transform.DOMove(
                    cars.GetActiveCar().transform.position + cars.GetActiveCar().transform.forward * DistanceToObject +
                    new Vector3(0, 5, 0), 0.5f);
            }
            else
                Camera.transform.DOMove(
                    cars.GetActiveCar().transform.position + cars.GetActiveCar().transform.forward * -DistanceToObject +
                    new Vector3(0, 5, 0), 0.5f);
        }

        if (Target)
        {
            Camera.transform.DOLookAt(Target.transform.position + new Vector3(0, 2, 0), 0.5f);
            if (IsNegativeZ)
            {
                Camera.transform.DOMove(
                    Target.transform.position + Target.transform.forward * DistanceToObject + new Vector3(0, 5, 0),
                    0.5f);
            }
            else
                Camera.transform.DOMove(
                    Target.transform.position + Target.transform.forward * -DistanceToObject + new Vector3(0, 5, 0),
                    0.5f);
        }
    }

    public void SetTargetToFollow(GameObject target)
    {
        this.Target = target;
    }

    public void SetNegativeZ(bool state)
    {
        IsNegativeZ = state;
    }
}