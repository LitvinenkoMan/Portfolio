using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] private List<Behaviour> componentsToDisable;

    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    //private GameObject ObjectCamera;
    private Camera camera;

    void Start()
    {
        DisableComponents();
        if (IsLocalPlayer)
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
        if (IsServer)
        {
            
        }

        if (!IsLocalPlayer)
        {
            foreach (var item in componentsToDisable)
            {
                item.enabled = false;
            }
            Debug.Log("Components disabled");
        }
    }

    private void Update()
    {
        if (IsServer)
        {
           
        }

        if (IsClient && IsOwner)
        {
            SubmitPositionRequestClientRpc();
        }
    }

    [ClientRpc]
    void SubmitPositionRequestClientRpc(ServerRpcParams rpcParams = default)
    {
        if (gameObject.transform.position != Position.Value)
        {
            Position.Value = gameObject.transform.position;
        }
    }
}