using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionChange : MonoBehaviour
{
    private Color color;
    private Material mat;
    public float t;
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        color = mat.color;
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Lerp(1.5f, 4f, Mathf.PingPong(Time.time, 1));
        mat.SetVector("_EmissionColor", color * t);
    }
}
