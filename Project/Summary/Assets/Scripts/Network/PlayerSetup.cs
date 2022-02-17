using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] private List<Behaviour> componentsToDisable;
    //public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    private GameObject ObjectCamera;
    private Camera camera;

    void Start()
    {
        DisableComponents();
        //Camera.main.GetComponent<CameraFolow>().SetTargetToFollow(gameObject);
        
    }

    public void DisableComponents()
    {
        if (!IsLocalPlayer)
        {
            foreach (var item in componentsToDisable)
            {
                item.enabled = false;
            }
            Debug.Log("Components disabled");
            ObjectCamera = new GameObject();
            ObjectCamera.AddComponent<Camera>();
            ObjectCamera.AddComponent<CameraFolow>().SetTargetToFollow(gameObject);
            ObjectCamera.GetComponent<CameraFolow>().SetNegativeZ(false);
        }
    }

    private void Update()
    {
        //transform.position = Position.Value;
    }
    
    [ServerRpc]
    void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
    {
        //Position.Value = transform.position + transform.forward * NetworkManager.LocalTime.Tick;
    }
}