using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField]private Material StartMaterial;
    [SerializeField]private Material[] ChangedMaterials;
    private MeshRenderer mesh;

    private void OnEnable()
    {
        if (mesh)
        {
            mesh.sharedMaterial = StartMaterial;
        }
    }

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

    private void OnMouseOver()
    {
        ChangeMaterial(0);
    }

    private void OnMouseDown()
    {
        ChangeMaterial(1);
    }

    private void OnMouseExit()
    {
        SetStartMaterial();
    }
    
    
}
