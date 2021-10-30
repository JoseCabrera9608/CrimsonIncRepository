using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomodoController : MonoBehaviour
{
    public bool lanzamientoMisiles = false;
    public GameObject nube;
    public bool lanzarNube;
    Transform target;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        if(lanzarNube == true)
        {
            StartCoroutine(LanzarNube());
            lanzarNube = false;
        }
        
    }

    IEnumerator LanzarNube()
    {
        nube.SetActive(true);
        yield return new WaitForSeconds(8);
        nube.SetActive(false);
    }
}
