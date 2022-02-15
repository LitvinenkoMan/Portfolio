using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    [SerializeField] private Vector3 MoveTo;
    [SerializeField] private Vector3 RotateTo;
    [SerializeField] private float duration;
    [SerializeField] private Camera camera;

    public void SetRotationToRotate(Vector3 rotation)
    {
        RotateTo = rotation;
    }

    public void SetPositionToMove(Vector3 position)
    {
        MoveTo = position;
    }

    public void ChangePosition()
    {
        camera?.gameObject.transform.DOMove(MoveTo, duration);
    }

    public void ChangeRotation()
    {
        camera?.gameObject.transform.DORotate(RotateTo, duration);
    }
}