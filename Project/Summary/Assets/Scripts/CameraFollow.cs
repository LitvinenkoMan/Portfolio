using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject Target;
    Camera Camera;
    [Inject] CarManager cars;

    [SerializeField] bool IsThirdPersonMode = false;
    [SerializeField] bool IsNegativeZ = true;

    [SerializeField] Vector3 OrthographicCameraPosition = new Vector3(80, 110, 80);
    [SerializeField] Vector3 OrthographRotation = new Vector3(45, -135, 0);
    [SerializeField] float DistanceToObject = 10;
    [SerializeField] private float OrthographicSize = 50;

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

        if (!IsThirdPersonMode)
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
                Camera.orthographicSize = OrthographicSize;
            }
        }
        else ThirdPersonMode();
    }

    void ChangeCameraView()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (IsThirdPersonMode)
            {
                Camera.orthographic = true;
                IsThirdPersonMode = false;
            }
            else
            {
                Camera.orthographic = false;
                IsThirdPersonMode = true;
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

    public void SetParametres(float distance, float orthographicSize, bool isNegativeZ, bool isThirdPersonMode)
    {
        DistanceToObject = distance;
        OrthographicSize = orthographicSize;
        IsNegativeZ = isNegativeZ;
        IsThirdPersonMode = isThirdPersonMode;

    }
}