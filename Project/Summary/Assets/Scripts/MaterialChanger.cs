using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour, ISubscriber
{
    [SerializeField]private Material StartMaterial;
    [SerializeField]private Material[] ChangedMaterials;
    private MeshRenderer mesh;
    void Start()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
        if (!StartMaterial)
        {
            StartMaterial = mesh.sharedMaterial;
        }
    }

    public void ChangeMaterial(int materialIndex)
    {
        mesh.sharedMaterial = ChangedMaterials[materialIndex];
    }

    public void SetStartMaterial()
    {
        mesh.sharedMaterial = StartMaterial;
    }

    public void ChangeMaterial(Material material)
    {
        mesh.sharedMaterial = material;
    }
    //TODO: переделать систему событий
    public void DoAction()
    {
       
    }
}
