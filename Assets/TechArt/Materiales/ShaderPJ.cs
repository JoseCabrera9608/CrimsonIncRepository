using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderPJ : MonoBehaviour
{
    public Material material;

    public string [] properties;

    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            material.SetFloat("Vector1_988d33b1a4aa437098aaf88e1a4809f2", -4f);
            Debug.Log("K");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            material.SetFloat("Vector1_988d33b1a4aa437098aaf88e1a4809f2", 3f);
            Debug.Log("K");
        }
    }
}
