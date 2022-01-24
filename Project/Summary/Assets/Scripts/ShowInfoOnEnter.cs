using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using TMPro;

public class ShowInfoOnEnter : MonoBehaviour
{
    [Inject] CarManager _cars;

    [SerializeField] GameObject description;

    [SerializeField] float moveOn;

    float _textStartPosition;
    bool _isEntered;

    private void Start()
    {
        _textStartPosition = gameObject.transform.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.IsChildOf(_cars.GetActiveCar().transform) && !_isEntered)
        {
            description.transform.DOMoveY(_textStartPosition + moveOn, 1.5f);
            _isEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.IsChildOf(_cars.GetActiveCar().transform) && _isEntered)
        {
            description.transform.DOMoveY(_textStartPosition - moveOn, 1.5f);
            _isEntered = false;
        }
    }
}