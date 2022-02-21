using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using DG.Tweening;
using Zenject;

public class DayLightChanger : MonoBehaviour
{
    [SerializeField] Light Light;

    [SerializeField] Color startColor;
    [Inject] CarManager cars;


    private void Start()
    {
        startColor = Color.white;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.IsChildOf(cars.GetActiveCar().transform))
            Light.gameObject.transform.DORotate(new Vector3(-105, 0, 0), 2);
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.gameObject.transform.IsChildOf(cars.GetActiveCar().transform))
            Light.gameObject.transform.DORotate(new Vector3(75, -90, -90), 2);
    }
}