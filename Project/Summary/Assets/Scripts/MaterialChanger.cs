using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField]private Material StartMaterial;
    [SerializeField]private Material ChangedMaterial;
    private MeshRenderer mesh;
    void Start()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
        if (!StartMaterial)
        {
            StartMaterial = mesh.sharedMaterial;
        }
    }

    private void Update()
    {
        //SetStartMaterial();
    }

    public void ChangeMaterial()
    {
        mesh.sharedMaterial = ChangedMaterial;
    }

    public void SetStartMaterial()
    {
        mesh.sharedMaterial = StartMaterial;
    }

    public void ChangeMaterial(Material material)
    {
        mesh.sharedMaterial = material;
    }
}
