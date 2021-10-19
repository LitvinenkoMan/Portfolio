using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class CameraFolow : MonoBehaviour
{

    CarMovment car;

    [Inject]
    void Constract(CarMovment car) 
    {
        this.car = car;
    }

    private void Update()
    {
        gameObject.transform.DOMove(car.transform.position + new Vector3(80, 110, 80), 0.5f); 
    }
}
