using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEstacaBoss : MonoBehaviour
{
    public GameObject esferaStun;
    public float timer;
    bool activarEsfera;
    void Start()
    {
       
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5)
        {
            activarEsfera = true;
        }

        if(activarEsfera == true)
        {
            esferaStun.SetActive(true);
            activarEsfera = false;
            timer = 0;
        }
        
    }

}
