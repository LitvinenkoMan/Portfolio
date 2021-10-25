using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLights : MonoBehaviour, ICarLightable
{
    [SerializeField]
    Light[] FrontLights;

    [SerializeField]
    Light[] BackLights;

    bool isFrontLightsActivated = false;
    bool isBackLightsActivated = false;
    void Update()
    {
        if (isFrontLightsActivated)
            DeactivateFrontLights();
        else ActivateFrontLights();

        if (isBackLightsActivated)
            DeactivateBackLights();
        else ActivateBackLights();
    }

    public void ActivateBackLights()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            for (int i = 0; i < BackLights.Length; i++)
            {
                BackLights[i].enabled = true;
                isBackLightsActivated = true;
            }
        }
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
        if (Input.GetAxis("Vertical") >= 0)
        {
            for (int i = 0; i < BackLights.Length; i++)
            {
                BackLights[i].enabled = false;
                isBackLightsActivated = false;
            }
        }
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

    public bool GetFrontLightsState() 
    {
        return isFrontLightsActivated;
    }

    public bool GetBackLightsState()
    {
        return isBackLightsActivated;
    }
}
