using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarElectricidadEFecto : MonoBehaviour
{
    public GameObject sueloElectrificadoObject;
    SueloElectrificado sueloScript;
    ParticleSystem esteSistema;

    void Start()
    {
        sueloScript = sueloElectrificadoObject.GetComponent<SueloElectrificado>();
        esteSistema = this.GetComponent<ParticleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sueloScript.electrocutar == true && !esteSistema.isPlaying)
        {
           
            esteSistema.Play();
        }
        else
        {
            return;
        }
    }
}
