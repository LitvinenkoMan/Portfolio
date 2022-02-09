using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCaster : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Ray _ray;
    private RaycastHit _raycastHit;
    private MaterialChanger materialChanger = null;

    void Start()
    {
        if (!cam)
        {
            cam = gameObject.GetComponent<Camera>();
        }
    }

    void Update()
    {
        _ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _raycastHit))
        {

            if (_raycastHit.collider.gameObject.GetComponent<MaterialChanger>())
            {
                materialChanger = _raycastHit.collider.gameObject.GetComponent<MaterialChanger>();
                _raycastHit.collider.gameObject.GetComponent<MaterialChanger>().ChangeMaterial();
            }

            if (!_raycastHit.collider.gameObject.GetComponent<MaterialChanger>() && materialChanger)
            {
                materialChanger.SetStartMaterial();
            }
        }
    }
}
