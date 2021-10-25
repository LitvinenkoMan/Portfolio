using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField, Min(1)]
    int ActiveCarNumber = 1;

    [SerializeField]
    CarMovment[] CarsMovments;

    [SerializeField]
    GameObject[] Cars;

    GameObject ActiveCar;

    bool IsInGarage;

    void Start()
    {
        ActiveCar = Cars[0];
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
            if (i == ActiveCarNumber - 1)
            {
                Cars[i].transform.rotation = ActiveCar.transform.rotation;
                Cars[i].transform.position = ActiveCar.transform.position;
                ActiveCar = Cars[i];
                Cars[i].SetActive(true);
            }
            else Cars[i].SetActive(false);
        }
    }

    void ChangeCar()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && IsInGarage)
        {
            if (ActiveCarNumber == Cars.Length)
            {
                ActiveCarNumber = 1;
            }
            else
            {
                ActiveCarNumber++;
            }
            SetActiveCar();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("In trigger");
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
}
