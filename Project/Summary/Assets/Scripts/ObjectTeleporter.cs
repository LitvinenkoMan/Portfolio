using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using Zenject;

public class ObjectTeleporter : MonoBehaviour
{
    [Inject] private CarManager _carManager;
    
    [SerializeField] private GameObject ObjectToTeleport;
    [SerializeField] private GameObject PlaceToTeleport;
    [SerializeField] private Vector3 TeleportPosition;
    [SerializeField, Space] private bool TeleportOnTrigger;
    [SerializeField] private float TeleportDeley;
    private float timer;
    private bool ObjectEnteredTrigger;

    void Start()
    {
        timer = 0;
        ObjectToTeleport = _carManager.GetActiveCar();
    }

    void Update()
    {
        if (ObjectEnteredTrigger && TeleportOnTrigger)
        {
            timer += Time.deltaTime;
            if (timer > TeleportDeley)
            {
                TeleportObject();
                timer = 0;
                ObjectEnteredTrigger = false;
            }
        }
    }

    public void TeleportObject()
    {
        if (ObjectToTeleport && PlaceToTeleport)
        {
            ObjectToTeleport.transform.position = PlaceToTeleport.transform.position;
            ObjectToTeleport.transform.rotation = PlaceToTeleport.transform.rotation;
        }
        else if (ObjectToTeleport)
        {
            ObjectToTeleport.transform.position = TeleportPosition;
        }
        ObjectToTeleport = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TeleportOnTrigger)
        {
            if (other.gameObject.transform.parent == _carManager.GetActiveCar().transform)
            {
                ObjectToTeleport = _carManager.GetActiveCar();
            }
            if (!ObjectToTeleport)
            {
                ObjectToTeleport = other.gameObject;
            }
            ObjectEnteredTrigger = true;
        }
    }
}