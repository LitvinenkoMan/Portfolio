using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Button3D : MonoBehaviour
{
    [Inject] private MenuManager _menuManager;

    [Inject] private CameraRayCaster _cameraRayCaster;

    public UnityEvent OnClick;

    private void OnMouseDown()
    {
        OnClick?.Invoke();
    }
}