using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTargetVfxScript : MonoBehaviour
{
    public GameObject TargetVfx;
    public GameObject Gorgonopsia;
    GorgonopsiaBoss GorgoScript;
    void Start()
    {
        GorgoScript = Gorgonopsia.GetComponent<GorgonopsiaBoss>();

    }

    // Update is called once per frame
    void Update()
    {
        if(GorgoScript.activateTargetVfx == true)
        {
            TargetVfx.SetActive(true);
        }

        if (GorgoScript.activateTargetVfx == false)
        {
            TargetVfx.SetActive(false);
        }
    }
}
