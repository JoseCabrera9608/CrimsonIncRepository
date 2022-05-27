using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{

    public float timer;
    public float cd;
    public GameObject firepoint;
    public GameObject fire;
    public GameObject fireSpawned;
    public float firelifetime;
    public float intervalo;
    public CapsuleCollider col;

    // Start is called before the first frame update
    void Start()
    {
        timer = firelifetime + intervalo;
        cd = firelifetime + intervalo;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        FireSpawn();
    }
    public void FireSpawn()
    {

        if (firepoint != null && timer >= cd)
        {



            fire.SetActive(true);

            timer = 0;
        }

        if (firelifetime < timer)
        {
            fire.SetActive(false);
        }

        if (timer > 1)
        {
            //col.enabled = true;
        }

    }
}
