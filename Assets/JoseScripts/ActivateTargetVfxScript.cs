using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTargetVfxScript : MonoBehaviour
{
    public GameObject TargetVfx;
    public GameObject bombaJaeger;
    BombaJaeger bombaJaegerScript;
    void Start()
    {
        bombaJaegerScript = bombaJaeger.GetComponent<BombaJaeger>();

    }

    // Update is called once per frame
    void Update()
    {
        if(bombaJaegerScript.activateTargetVfx == true)
        {
            TargetVfx.SetActive(true);
        }

        if (bombaJaegerScript.activateTargetVfx == false)
        {
            TargetVfx.SetActive(false);
        }
    }
}
