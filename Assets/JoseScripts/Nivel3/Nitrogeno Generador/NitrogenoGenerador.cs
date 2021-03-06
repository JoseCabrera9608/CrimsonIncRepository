using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitrogenoGenerador : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject colliderHumo;
    ParticleSystem thisSystem;
    public float timerNitrogeno;

    void Start()
    {
        thisSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timerNitrogeno += Time.deltaTime;

        if(timerNitrogeno >= 8)
        {
            colliderHumo.SetActive(true);

        }
        if(timerNitrogeno >= 13)
        {
            colliderHumo.SetActive(false);

        }
        if(timerNitrogeno >= 14.66f)
        {
            timerNitrogeno = 0;
            thisSystem.Play();
             
        }
    }
}
