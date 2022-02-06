using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLights : MonoBehaviour, ICarLightable
{
    [SerializeField] Material frontLightsMaterial;
    [SerializeField] Material backLightsMaterial;
    [SerializeField] Light[] frontLights;
    [SerializeField] Light[] backLights;

    bool _isFrontLightsActivated = false;
    bool _isBackLightsActivated = false;
    Renderer FLMRenderer;
    Renderer BLMRenderer;

    void Start()
    {
        FLMRenderer = GetComponent<Renderer>();
        BLMRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (_isFrontLightsActivated)
            DeactivateFrontLights();
        else ActivateFrontLights();

        if (_isBackLightsActivated)
            DeactivateBackLights();
        else ActivateBackLights();
    }

    public void ActivateBackLights()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            if (backLightsMaterial) backLightsMaterial.EnableKeyword("_EMISSION");
            for (int i = 0; i < backLights.Length; i++)
            {
                backLights[i].enabled = true;
                _isBackLightsActivated = true;
            }
        }
    }

    public void ActivateFrontLights()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (frontLightsMaterial) frontLightsMaterial.EnableKeyword("_EMISSION");
            for (int i = 0; i < frontLights.Length; i++)
            {
                frontLights[i].enabled = true;
                _isFrontLightsActivated = true;
            }
        }
    }

    public void DeactivateBackLights()
    {
        if (Input.GetAxis("Vertical") >= 0)
        {
            if (backLightsMaterial) backLightsMaterial.DisableKeyword("_EMISSION");
            for (int i = 0; i < backLights.Length; i++)
            {
                backLights[i].enabled = false;
                _isBackLightsActivated = false;
            }
        }
    }

    public void DeactivateFrontLights()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (frontLightsMaterial) frontLightsMaterial.DisableKeyword("_EMISSION");
            for (int i = 0; i < frontLights.Length; i++)
            {
                frontLights[i].enabled = false;
                _isFrontLightsActivated = false;
            }
        }
    }

    public bool GetFrontLightsState()
    {
        return _isFrontLightsActivated;
    }

    public bool GetBackLightsState()
    {
        return _isBackLightsActivated;
    }
}