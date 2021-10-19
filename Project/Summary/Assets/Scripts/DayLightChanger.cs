using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using DG.Tweening;
using Zenject;

public class DayLightChanger : MonoBehaviour
{
    [SerializeField]
    Light Light;

    [SerializeField]
    Color startColor;

    CarMovment car;

    [Inject]
    void Constract(CarMovment car) 
    {
        this.car = car;
    }

    private void Start()
    {
        startColor = Color.white;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.IsChildOf(car.gameObject.transform))
            Light.DOColor(Color.black, 2f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.IsChildOf(car.gameObject.transform))
            Light.DOColor(startColor, 2f);
    }
}
