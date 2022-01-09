using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NCMCExecuter : MonoBehaviour
{
    [SerializeField]
    NonConvexMeshCollider NonConvexMeshCollider;
    void Start()
    {
        NonConvexMeshCollider.Calculate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
