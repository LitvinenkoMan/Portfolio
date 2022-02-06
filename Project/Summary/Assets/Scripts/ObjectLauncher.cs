using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ObjectLauncher : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToLaunch;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float power;
    [SerializeField] private float delay;
    [SerializeField] private GameObject target;
    [SerializeField] private bool launchOnTrigger;
    private Rigidbody gameObjectToLaunchRigidbody;
    private bool readyToLaunch;
    private float timer;

    void Start()
    {
        timer = 0;
    }

    public void FixedUpdate()
    {
        if (readyToLaunch)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                LaunchObject();
                readyToLaunch = false;
                timer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (launchOnTrigger && !gameObjectToLaunch)
        {
            gameObjectToLaunch = other.gameObject;
            if (gameObjectToLaunch)
                if (gameObjectToLaunch.GetComponent<Rigidbody>())
                    gameObjectToLaunchRigidbody = gameObjectToLaunch.GetComponent<Rigidbody>();
                else
                    gameObjectToLaunchRigidbody = gameObjectToLaunch.GetComponentInParent<Rigidbody>();
            readyToLaunch = true;
        }
    }

    public void LaunchObject()
    {
        if (target && gameObjectToLaunchRigidbody)
        {
            //gameObjectToLaunchRigidbody.(target.transform.position);
        }
        else if (gameObjectToLaunchRigidbody)
        {
            gameObjectToLaunchRigidbody.AddForce(direction * power * gameObjectToLaunchRigidbody.mass);
            gameObjectToLaunch = null;
            readyToLaunch = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + direction);
    }
}