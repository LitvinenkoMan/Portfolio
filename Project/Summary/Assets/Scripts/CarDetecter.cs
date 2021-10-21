using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using TMPro;

public class CarDetecter : MonoBehaviour
{
    CarManager cars;

    [SerializeField]
    GameObject Description;

    [SerializeField]
    float textStartPosition;
    [SerializeField]
    float textEndPosition;

    bool IsEntered;

    bool IsInGarage;
    

    [Inject]
    void Constract(CarManager cars) 
    {
        this.cars = cars;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.IsChildOf(cars.GetActiveCar().transform) && !IsEntered)
        { 
            Description.transform.DOMoveY(textEndPosition, 1.5f);
            IsEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.IsChildOf(cars.GetActiveCar().transform) && IsEntered)
        {
            Description.transform.DOMoveY(textStartPosition, 1.5f);
            IsEntered = false;
        }
    }
}
