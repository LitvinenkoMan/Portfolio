using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using TMPro;

public class CarDetecter : MonoBehaviour
{
    CarMovment car;
    [SerializeField]
    GameObject Description;

    [SerializeField]
    float textStartPosition;
    [SerializeField]
    float textEndPosition;

    bool IsEntered;
    

    [Inject]
    void Constract(CarMovment car) 
    {
        this.car = car;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.IsChildOf(car.gameObject.transform) && !IsEntered)
        { 
            Description.transform.DOMoveY(textEndPosition, 1.5f);
            IsEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.IsChildOf(car.gameObject.transform) && IsEntered)
        {
            Description.transform.DOMoveY(textStartPosition, 1.5f);
            IsEntered = false;
        }
    }
}
