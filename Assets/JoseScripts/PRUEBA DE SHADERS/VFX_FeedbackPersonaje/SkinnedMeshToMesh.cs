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
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(UpdateVfxGraphs());
    }
    IEnumerator UpdateVfxGraphs()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(refreshRate);
        }
        
    }
}
