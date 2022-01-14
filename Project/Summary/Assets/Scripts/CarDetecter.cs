using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using TMPro;

public class CarDetecter : MonoBehaviour
{
    [Inject]
    CarManager cars;

    [SerializeField]
    GameObject Description;

    float textStartPosition;
    [SerializeField]
    float MoveOn;

    bool IsEntered;

    private void Start()
    {
        textStartPosition = gameObject.transform.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.IsChildOf(cars.GetActiveCar().transform) && !IsEntered)
        {
            Description.transform.DOMoveY(textStartPosition + MoveOn, 1.5f);
            IsEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.IsChildOf(cars.GetActiveCar().transform) && IsEntered)
        {
            Description.transform.DOMoveY(textStartPosition - MoveOn, 1.5f);
            IsEntered = false;
        }
    }
}
