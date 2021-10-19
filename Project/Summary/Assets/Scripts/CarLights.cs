using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLights : MonoBehaviour, ICarLightable
{
    [SerializeField]
    public Light[] FrontLights;

    [SerializeField]
    public Light[] backLights;

    bool isFrontLightsActivated = false;
    void Update()
    {
        if (isFrontLightsActivated )
            DeactivateFrontLights();
        else ActivateFrontLights();
    }

    public void ActivateBackLights()
    {
       
    }

    public void ActivateFrontLights()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            for (int i = 0; i < FrontLights.Length; i++)
            {
                FrontLights[i].enabled = true;
                isFrontLightsActivated = true;
            }
        }
    }

    public void DeactivateBackLights()
    {
       
    }

    public void DeactivateFrontLights()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            for (int i = 0; i < FrontLights.Length; i++)
            {
                FrontLights[i].enabled = false;
                isFrontLightsActivated = false;
            }
        }
    }

}
