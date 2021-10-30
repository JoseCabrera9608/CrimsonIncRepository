using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NubeCorrosiva : Habilidad_SO
{
    GameObject nube;
    float timer;
    bool activar = false;

    public override void Activate(GameObject parent)
    {
        nube = GameObject.FindGameObjectWithTag("Nube");
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            activar = true;
            if(timer >= 4)
            {
                activar = false;
                timer = 0;
            }
        }
        if(activar == true)
        {
            nube.SetActive(true);
        }
        if(activar == false)
        {
            nube.SetActive(false);
        }
    }

}
