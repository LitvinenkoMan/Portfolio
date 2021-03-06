using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCaster : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Ray _ray;
    private RaycastHit _raycastHit;
    private MaterialChanger materialChanger = null;

    delegate void MouseClick();
    event MouseClick OnMouseClick;

    delegate void MouseDrag();
    event MouseDrag OnMouseDrag;

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
            
            OnMouseDrag?.Invoke();
            if (_raycastHit.collider.gameObject.GetComponent<MaterialChanger>())
            {
                //materialChanger = _raycastHit.collider.gameObject.GetComponent<MaterialChanger>();
                _raycastHit.collider.gameObject.GetComponent<MaterialChanger>().ChangeMaterial(0);
            }
            if (_raycastHit.collider.gameObject.GetComponent<MaterialChanger>() && Input.GetMouseButton(0))
            {
                OnMouseClick?.Invoke();
                //materialChanger = _raycastHit.collider.gameObject.GetComponent<MaterialChanger>();
                //_raycastHit.collider.gameObject.GetComponent<MaterialChanger>().ChangeMaterial(1);
                if (_raycastHit.collider.gameObject.GetComponent<CameraMove>())
                {
                    _raycastHit.collider.gameObject.GetComponent<CameraMove>().ChangePosition();
                    _raycastHit.collider.gameObject.GetComponent<CameraMove>().ChangeRotation();
                }
            }

            /*if (!_raycastHit.collider.gameObject.GetComponent<MaterialChanger>() && materialChanger)
            {
                materialChanger.SetStartMaterial();
            }*/
        }
    }
}
