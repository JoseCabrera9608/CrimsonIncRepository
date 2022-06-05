using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SkinnedMeshToMesh : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;
    public VisualEffect VFXGraphs;
    public float refreshRate;
    void Start()
    {
        StartCoroutine(UpdateVfxGraphs());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator UpdateVfxGraphs()
    {
        while (gameObject.activeSelf)
        {
            Mesh m = new Mesh();
            skinnedMesh.BakeMesh(m);
            VFXGraphs.SetMesh("Mesh", m);
            yield return new WaitForSeconds(refreshRate);
        }
        
    }
}
