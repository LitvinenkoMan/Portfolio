using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class PlayerSetup : MonoBehaviourPun
{
    [SerializeField] private List<Behaviour> componentsToDisable;

    [SerializeField] private PhotonView _pView;
    
    private Camera camera;

    void Start()
    {
        DisableComponents();
        if (_pView.IsMine)
        {
            GameObject ObjectCamera = Instantiate(new GameObject(), transform.position, transform.rotation);
            ObjectCamera.name = $"Camera {gameObject.name}";
            ObjectCamera.AddComponent<Camera>();
            ObjectCamera.AddComponent<CameraFollow>().SetTargetToFollow(gameObject);
            ObjectCamera.GetComponent<CameraFollow>().SetParametres(10, 50, false, false);
        }
    }

    public void DisableComponents()
    {
        if (!_pView.IsMine)
        {
            foreach (var item in componentsToDisable)
            {
                item.enabled = false;
            }
            Debug.Log("Components disabled");
        }
    }
    
    [PunRPC]
    public void SetPositionAndRotation(Vector3 position, Vector3 rotation)
    {
        gameObject.transform.position = position;
        gameObject.transform.Rotate(rotation);
    }
}
