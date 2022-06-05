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
            Vector3[] vertices = m.vertices;
            Mesh m2 = new Mesh();
            m2.vertices = vertices;
            yield return new WaitForSeconds(refreshRate);
        }
        
    }
}
