using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CarManager : MonoBehaviour
{
    [SerializeField, Min(1)]
    int ActiveCarNumber = 1;
    [SerializeField]
    bool ChangeCarWithoutTrigger = false;

    [SerializeField,]
    CarMovment[] CarsMovments;

    [SerializeField]
    GameObject[] Cars;

    GameObject ActiveCar;
    CarMovment ActiveCarMovment;

    bool IsInGarage;



    void Start()
    {
        ActiveCar = Cars[ActiveCarNumber-1];
        SetActiveCar();
    }

    void Update()
    {
        ChangeCar();
    }

    void SetActiveCar()
    {
        for (int i = 0; i < Cars.Length; i++)
        {
            float priveiosCarSpeed = ActiveCar.GetComponent<CarSpeed>().GetCurrentSpeed();
            if (i == ActiveCarNumber - 1)
            {
                Cars[i].transform.rotation = ActiveCar.transform.rotation;
                Cars[i].transform.position = ActiveCar.transform.position;
                ActiveCar = Cars[i];
                ActiveCarMovment = CarsMovments[i];
                Cars[i].SetActive(true);
                if (Cars[i].GetComponent<CarSpeed>())
                {
                    Cars[i].GetComponent<Rigidbody>().AddForce(-Cars[i].transform.forward * priveiosCarSpeed * 10000);
                }
            }
            else 
            {
                Cars[i].SetActive(false);
            } 
        }
        if (ActiveCarNumber == Cars.Length)
        {
            ActiveCarNumber = 1;
        }
        else
        {
            ActiveCarNumber++;
        }
    }

    void ChangeCar()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && IsInGarage)
        {
            SetActiveCar();
        }
        if (Input.GetKeyDown(KeyCode.Tab) && ChangeCarWithoutTrigger)
        {
            SetActiveCar();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.IsChildOf(ActiveCar.transform))
        {
            ChangeCar();
        }
    }

    public void SetInGarageState(bool TorF)
    {
        IsInGarage = TorF;
    }

    public bool CheckInGarage()
    {
        return IsInGarage;
    }

    public GameObject GetActiveCar()
    {
        return ActiveCar;
    }

    public CarMovment GetActiveCarMovment()
    {
        return ActiveCarMovment;
    }
}
