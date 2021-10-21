using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GarageDetecter : MonoBehaviour
{
    [Inject]
    CarManager CarManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.IsChildOf(CarManager.GetActiveCar().transform))
        {
            CarManager.SetInGarageState(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.IsChildOf(CarManager.GetActiveCar().transform))
        {
            CarManager.SetInGarageState(false);
        }
    }
}
